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
            return $"Wind (gust) m/s {Math.Round(Speed)} ({Math.Round(Gust)}) " +
                $"direction: {Utilities.ConvertCompassDegToStringDirection(DirectionDegrees)}";
        }
    }
}
