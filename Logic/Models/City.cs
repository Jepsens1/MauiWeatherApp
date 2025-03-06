using Newtonsoft.Json;

namespace WeatherApp.Logic.Models
{
    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("timezone")]
        public int TimezoneUTC { get; set; }

        [JsonProperty("sunrise")]
        public int SunriseUTC { get; set; }

        [JsonProperty("sunset")]
        public int SunsetUTC { get; set; }

        public override string ToString()
        {
            return $"{Coord}\nTimezone: {TimezoneUTC}\nSunrise: {Utilities.UnixTimeStampToDateTimeString(SunriseUTC)}" +
                $" - Sunset: {Utilities.UnixTimeStampToDateTimeString(SunsetUTC)}"; //ToString handles null check
        }
    }
}
