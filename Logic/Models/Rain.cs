using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Rain
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }

        public override string ToString()
        {
            return $"Rain Precipitation: {Precipitation}mm/h";
        }
    }
}
