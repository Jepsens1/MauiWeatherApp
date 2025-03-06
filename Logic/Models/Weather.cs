using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Weather
    {
        [JsonProperty("id")]
        public int WeatherConditionIdentifier { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonIgnore()]
        public string IconImage { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Main}\t{Description}";
        }
    }
}
