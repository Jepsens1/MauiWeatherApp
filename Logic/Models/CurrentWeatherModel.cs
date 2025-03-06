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

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("snow")]
        public Snow Snow { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Coord}\n{Main}\n{Wind}\n" +
                $"Visibility: {Visibility / 1000}km\n{Rain}\t{Snow}"; //ToString handles null check
        }
    }
}
