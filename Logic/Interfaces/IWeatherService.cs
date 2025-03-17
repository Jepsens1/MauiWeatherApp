using WeatherApp.Logic.Models;

namespace WeatherApp.Logic.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherDataAsync(params string[] locationParams);
    }
}
