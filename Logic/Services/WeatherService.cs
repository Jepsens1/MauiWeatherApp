using Newtonsoft.Json;
using WeatherApp.Logic.Interfaces;
using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Services
{
    public class WeatherService : IWeatherService
    {
        private HttpClient m_httpClient;
        private string m_API_KEY = string.Empty;
        public WeatherService(HttpClient httpClient)
        {
            m_httpClient = httpClient;
            m_API_KEY = "";
        }
        public async Task<WeatherResponse> GetWeatherDataAsync(string lat, string lon)
        {
            var response = await m_httpClient.GetAsync($"{Constants.BASE_URL}?lat={lat}&lon={lon}&units=metric&&appid={m_API_KEY}");
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrWhiteSpace(data)) return new WeatherResponse{ IsSuccess = false, ErrorMessage = "Failed to read response content"};

                var weatherModel = JsonConvert.DeserializeObject<WeatherModel>(data);
                if (weatherModel is null) return new WeatherResponse { IsSuccess = false};

                return new WeatherResponse { IsSuccess = true, WeatherData = weatherModel };
            }
            return new WeatherResponse { IsSuccess = false, ErrorMessage = $"StatusCode: {response?.StatusCode} with response message: {response?.Content}"};
        }
    }
}
