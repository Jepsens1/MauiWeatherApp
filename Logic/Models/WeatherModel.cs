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

        [JsonProperty("name")]
        public string CityName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Coord}\n{Main}\n{Wind}\n" +
                $"Visibility: {Visibility / 1000}km\n{Rain}\t{Snow}"; //ToString handles null check
        }
    }

    public class Snow
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }

        public override string ToString()
        {
            return $"Snow Precipitation:{Precipitation}mm/h";
        }
    }
    public class Coord
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Math.Round(Latitude,2)}\tLongitude: {Math.Round(Longitude,2)}";
        }
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

        public override string ToString()
        {
            return $"Current Temperature: {Math.Round(Temperature)}°C\nFeels like: {Math.Round(Feelslike)}°C\n" +
                $"Temperature range: {Math.Round(MinimumTemperature)}-{Math.Round(MaximumTemperature)}°C\nHumidity: {Humidity}%";
        }
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double Precipitation { get; set; }

        public override string ToString()
        {
            return $"Rain Precipitation: {Precipitation}mm/h";
        }
    }
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
