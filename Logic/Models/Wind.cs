using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int DirectionDegrees { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }

        public override string ToString()
        {
            return $"Wind speed is at {Math.Round(Speed)}m/s with a gust of wind speed at {Math.Round(Gust)}m/s" +
                $" in the direction {Utilities.ConvertCompassDegToStringDirection(DirectionDegrees)}";
        }
    }
}
