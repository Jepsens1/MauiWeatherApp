using Newtonsoft.Json;
using System.Diagnostics;
using WeatherApp.Logic.Interfaces;
using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private HttpClient m_httpClient;
        private string m_API_KEY = string.Empty;
        public WeatherService(string api = "")
        {
            m_httpClient = new HttpClient();
            m_API_KEY = api;
        }
        /// <summary>
        /// Gets current and forecast weather data based on the location params
        /// </summary>
        /// <param name="locationParams"></param>
        /// <returns></returns>
        public async Task<WeatherResponse> GetWeatherDataAsync(params string[] locationParams)
        {
            try
            {
                var weatherResponse = new WeatherResponse { IsSuccess = true };
                var currentWeatherData = await GetCurrentWeatherDataAsync(locationParams);
                if(currentWeatherData is null)
                {
                    weatherResponse.CurrentWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage = "Failed to get current weather data\n";
                }
                else
                {
                    weatherResponse.CurrentWeatherData = currentWeatherData;
                }
                var forecastWeatherData = await GetForecastWeatherDataAsync(locationParams);
                if (forecastWeatherData is null)
                {
                    weatherResponse.ForecastWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage += "Failed to get forecast weather data";
                }
                else
                {
                    
                    var groupedData = new List<ForecastGroup>(
                        forecastWeatherData.Entries.OrderBy(f => f.TimeForecastedDateTime) //Order each timestamp
                        .GroupBy(f => f.TimeForecastedDateTime.Date) //Groups each timestamp to date
                        .OrderBy(g => g.Key) //Order by date
                        .Select(g => new ForecastGroup(g.Key.ToString("dddd MMMM dd, yyyy"), g))
                        );
                    weatherResponse.ForecastWeatherData = new ForecastDisplayModel
                    {
                        GroupedData = groupedData
                    };
                }
                return weatherResponse;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
                return new WeatherResponse { IsSuccess = false, ErrorMessage = "Unexpected error occured during getting data" };
            }
        }
        /// <summary>
        /// Makes a http GET request to Openweathermap current weather endpoint
        /// Deserialize the response to CurrentWeatherModel
        /// </summary>
        /// <param name="locationParameters"></param>
        /// <returns>nullable CurrentWeatherModel</returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<CurrentWeatherModel?> GetCurrentWeatherDataAsync(params string[] locationParameters)
        {
            string url = string.Empty;
            if (locationParameters.Length is 1) //City name provided
            {
                string city = locationParameters[0];
                url = $"{Constants.CURRENT_WEATHER_URL}?q={city}&units=metric&appid={m_API_KEY}";
            }
            else if (locationParameters.Length is 2) //Latitude and Longitude provided
            {
                var latitude = locationParameters[0];
                var longitude = locationParameters[1];
                url = $"{Constants.CURRENT_WEATHER_URL}?lat={latitude}&lon={longitude}&units=metric&appid={m_API_KEY}";
            }
            else
            {
                throw new ArgumentException("Invalid location parameters.");
            }

            var response = await m_httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(data))
                return null;

            var weatherModel = JsonConvert.DeserializeObject<CurrentWeatherModel>(data);
            if (weatherModel is null) return null;

            weatherModel.Weather.ForEach(x => x.IconImage = $"{Constants.ICON_URL}{x.Icon}@2x.png");
            return weatherModel;
        }
        /// <summary>
        /// Makes a http GET request to Openweathermap forecast weather endpoint
        /// Deserialize the response to ForecastWeatherModel
        /// </summary>
        /// <param name="locationParameters"></param>
        /// <returns>nullable ForecastWeatherModel</returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<ForecastWeatherModel?> GetForecastWeatherDataAsync(params string[] locationParameters)
        {
            string url = string.Empty;
            if(locationParameters.Length is 1) //City name provided
            {
                string city = locationParameters[0];
                url = $"{Constants.FORECAST_URL}?q={city}&units=metric&appid={m_API_KEY}";
            }
            else if (locationParameters.Length is 2) //Latitude and Longitude provided
            {
                var latitude = locationParameters[0];
                var longitude = locationParameters[1];
                url = $"{Constants.FORECAST_URL}?lat={latitude}&lon={longitude}&units=metric&appid={m_API_KEY}";
            }
            else
            {
                throw new ArgumentException("Invalid location parameters.");
            }

            var response = await m_httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(data))
                return null;

            var weatherModel = JsonConvert.DeserializeObject<ForecastWeatherModel>(data);
            if (weatherModel is null) return null;

            weatherModel.Entries.ForEach(entry => entry.Weather.ForEach(x => x.IconImage = $"{Constants.ICON_URL}{x.Icon}@2x.png"));
            return weatherModel;
        }
    }
}
