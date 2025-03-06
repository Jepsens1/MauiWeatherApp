using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double Feelslike { get; set; }

        [JsonProperty("temp_min")]
        public double MinimumTemperature { get; set; }

        [JsonProperty("temp_max")]
        public double MaximumTemperature { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        public override string ToString()
        {
            return $"Current Temperature: {Math.Round(Temperature)}°C\nFeels like: {Math.Round(Feelslike)}°C\n" +
                $"Temperature range: {Math.Round(MinimumTemperature)}-{Math.Round(MaximumTemperature)}°C\nHumidity: {Humidity}%";
        }
    }
}
