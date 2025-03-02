using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherApp.Logic.Models
{
    public class WeatherModel
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

        [JsonProperty("cloud")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public int DataCalculation { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }
    }

    public class Snow
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }
    }
    public class Clouds
    {
        [JsonProperty("all")]
        public int Cloudiness { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

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
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }
    }
    public class Weather
    {
        [JsonProperty("id")]
        public int WeatherConditionIdentifier { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int DirectionDegrees { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}
