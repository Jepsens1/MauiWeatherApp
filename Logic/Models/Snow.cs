﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WeatherApp.Logic.Models
{
    public class Snow
    {
        [JsonProperty("1h")]
        public double OneHourPrecipitation { get; set; }

        [JsonProperty("3h")]
        public double ThreeHourPrecipitation { get; set; }
    }
}
