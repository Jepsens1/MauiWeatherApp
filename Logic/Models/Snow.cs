using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Snow
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }

        public override string ToString()
        {
            return $"Snow Precipitation:{Precipitation}mm/h";
        }
    }
}
