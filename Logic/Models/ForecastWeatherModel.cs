using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class ForecastWeatherModel
    {
        [JsonProperty("list")]
        public List<ForecastEntry> Entries { get; set; }
    }
}
