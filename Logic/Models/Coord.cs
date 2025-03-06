using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Coord
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Math.Round(Latitude, 2)}\tLongitude: {Math.Round(Longitude, 2)}";
        }
    }
}
