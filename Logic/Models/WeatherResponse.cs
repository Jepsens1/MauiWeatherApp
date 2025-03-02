namespace WeatherApp.Logic.Models
{
    public class WeatherResponse
    {
        public WeatherModel WeatherData { get; set; }
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
