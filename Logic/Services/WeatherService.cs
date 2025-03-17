using Newtonsoft.Json;
using System.Diagnostics;
using WeatherApp.Logic.Interfaces;
using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Services
{
    public class WeatherService : IWeatherService, IDisposable
    {
        private HttpClient m_httpClient;
        private string m_API_KEY = string.Empty;
        public WeatherService(string api = "")
        {
            m_httpClient = new HttpClient();
            m_API_KEY = api;
        }

        public void Dispose()
        {
            m_httpClient.Dispose();
        }
        public async Task<WeatherResponse> GetWeatherDataAsync(string lat, string lon)
        {
            try
            {
                var weatherResponse = new WeatherResponse { IsSuccess = true };
                var currentWeatherData = await GetCurrentWeatherDataAsync(lat, lon);
                if (currentWeatherData is null)
                {
                    weatherResponse.CurrentWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage = "Failed to get current weather data\n";
                }
                else
                {
                    weatherResponse.CurrentWeatherData = currentWeatherData;
                }
                var forecastWeatherData = await GetForecastWeatherDataAsync(lat, lon);
                if (forecastWeatherData is null)
                {
                    weatherResponse.ForecastWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage += "Failed to get forecast weather data";
                }
                else
                {
                    var groupedData = new List<ForecastGroup>(
                        forecastWeatherData.Entries.OrderBy(f => f.TimeForecastedDateTime)
                        .GroupBy(f => f.TimeForecastedDateTime.Date)
                        .OrderBy(g => g.Key)
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

        public async Task<WeatherResponse> GetWeatherDataByNameAsync(string cityName)
        {
            try
            {
                var weatherResponse = new WeatherResponse { IsSuccess = true };
                var currentWeatherData = await GetCurrentWeatherDataByNameAsync(cityName);
                if (currentWeatherData is null)
                {
                    weatherResponse.CurrentWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage = "Failed to get current weather data\n";
                }
                else
                {
                    weatherResponse.CurrentWeatherData = currentWeatherData;
                }
                var forecastWeatherData = await GetForecastWeatherByNameDataAsync(cityName);
                if (forecastWeatherData is null)
                {
                    weatherResponse.ForecastWeatherData = null;
                    weatherResponse.IsSuccess = false;
                    weatherResponse.ErrorMessage += "Failed to get forecast weather data";
                }
                else
                {
                    var groupedData = new List<ForecastGroup>(
                        forecastWeatherData.Entries.OrderBy(f => f.TimeForecastedDateTime)
                        .GroupBy(f => f.TimeForecastedDateTime.Date)
                        .OrderBy(g => g.Key)
                        .Select(g => new ForecastGroup(g.Key.ToString("D"), g))
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


        private async Task<CurrentWeatherModel?> GetCurrentWeatherDataAsync(string lat, string lon)
        {
            if (!double.TryParse(lat, out double latitude) || !double.TryParse(lon, out double longitude))
                return null;

            var response = await m_httpClient.GetAsync($"{Constants.CURRENT_WEATHER_URL}?lat={latitude}&lon={longitude}&units=metric&appid={m_API_KEY}");
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
        private async Task<ForecastWeatherModel?> GetForecastWeatherDataAsync(string lat, string lon)
        {
            if (!double.TryParse(lat, out double latitude) || !double.TryParse(lon, out double longitude))
                return null;

            var response = await m_httpClient.GetAsync($"{Constants.FORECAST_URL}?lat={latitude}&lon={longitude}&units=metric&appid={m_API_KEY}");
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

        private async Task<CurrentWeatherModel?> GetCurrentWeatherDataByNameAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                return null;

            var response = await m_httpClient.GetAsync($"{Constants.CURRENT_WEATHER_URL}?q={city}&units=metric&appid={m_API_KEY}");
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
        private async Task<ForecastWeatherModel?> GetForecastWeatherByNameDataAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
                return null;

            var response = await m_httpClient.GetAsync($"{Constants.FORECAST_URL}?q={city}&units=metric&appid={m_API_KEY}");
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
