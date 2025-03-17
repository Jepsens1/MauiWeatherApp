using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    /// <summary>
    /// Model used for deserialize forecast data from openweathermap
    /// </summary>
    public class ForecastWeatherModel
    {
        [JsonProperty("list")]
        public List<ForecastEntry> Entries { get; set; }
    }
}
