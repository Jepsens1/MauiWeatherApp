using Newtonsoft.Json;

namespace WeatherApp.Logic.Models
{
    public class ForecastEntry
    {
        [JsonProperty("dt")]
        public int TimeForecastedUnixUTC { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("snow")]
        public Snow Snow { get; set; }

        public override string ToString()
        {
            return $"{TimeForecastedUnixUTC}\n{Main}\n{Wind}\n" +
                $"Visibility: {Visibility / 1000}km\n{Rain?.ThreeHourPrecipitation}\t" +
                $"{Snow?.ThreeHourPrecipitation}"; //ToString handles null check
        }
    }
}
