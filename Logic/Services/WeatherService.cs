using Newtonsoft.Json;
using WeatherApp.Logic.Interfaces;
using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Services
{
    public class WeatherService : IWeatherService, IDisposable
    {
        private HttpClient m_httpClient;
        private string m_API_KEY = string.Empty;
        public WeatherService(string api)
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
            if (!double.TryParse(lat, out double latitude) || !double.TryParse(lon, out double longitude))
                return new WeatherResponse { IsSuccess = false, ErrorMessage = "Only accept numbers in input field" };

            var response = await m_httpClient.GetAsync($"{Constants.CURRENT_WEATHER_URL}?lat={latitude}&lon={longitude}&units=metric&&appid={m_API_KEY}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(data)) return new WeatherResponse { IsSuccess = false, ErrorMessage = "Failed to read response content" };

                var weatherModel = JsonConvert.DeserializeObject<CurrentWeatherModel>(data);
                if (weatherModel is null) return new WeatherResponse { IsSuccess = false };

                weatherModel.Weather.First().IconImage = $"{Constants.ICON_URL}{weatherModel.Weather.First().Icon}@2x.png";
                return new WeatherResponse { IsSuccess = true, CurrentWeatherData = weatherModel };
            }
            return new WeatherResponse { IsSuccess = false, ErrorMessage = $"StatusCode: {response?.StatusCode}" };
        }

        public async Task<WeatherResponse> GetWeatherDataByNameAsync(string cityName)
        {
            var response = await m_httpClient.GetAsync($"{Constants.CURRENT_WEATHER_URL}?q={cityName}&units=metric&&appid={m_API_KEY}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(data)) return new WeatherResponse { IsSuccess = false, ErrorMessage = "Failed to read response content" };

                var weatherModel = JsonConvert.DeserializeObject<CurrentWeatherModel>(data);
                if (weatherModel is null) return new WeatherResponse { IsSuccess = false };

                weatherModel.Weather.First().IconImage = $"{Constants.ICON_URL}{weatherModel.Weather.First().Icon}@2x.png";
                return new WeatherResponse { IsSuccess = true, CurrentWeatherData = weatherModel };
            }
            return new WeatherResponse { IsSuccess = false, ErrorMessage = $"StatusCode: {response?.StatusCode}" };
        }
    }
}
