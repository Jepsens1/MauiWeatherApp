namespace WeatherApp.Logic.Models
{
    /// <summary>
    /// Represents a wrapper for storing both current and forecast data
    /// And in case of error during HTTP request a error message and a success flag
    /// </summary>
    public class WeatherResponse
    {
        public CurrentWeatherModel? CurrentWeatherData { get; set; }

        public ForecastDisplayModel? ForecastWeatherData { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
