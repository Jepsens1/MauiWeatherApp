using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class CurrentWeatherModel
    {
        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        public string VisibilityInKM => $"Visibility: {Visibility / 1000}";
        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("snow")]
        public Snow Snow { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; } = string.Empty;
    }
}
