using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Rain
    {
        [JsonProperty("1h")]
        public double OneHourPrecipitation { get; set; } = 0;

        [JsonProperty("3h")]
        public double ThreeHourPrecipitation { get; set; } = 0;
    }
}
