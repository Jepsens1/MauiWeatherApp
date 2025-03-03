using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeatherDataAsync(string lat, string lon);
        Task<WeatherResponse> GetCurrentWeatherDataByNameAsync(string cityName);
    }
}
