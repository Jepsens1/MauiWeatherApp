namespace WeatherApp.Logic.Models
{
    public class WeatherResponse
    {
        public CurrentWeatherModel CurrentWeatherData { get; set; }

        public ForecastWeatherModel ForecastWeatherData { get; set; }
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
