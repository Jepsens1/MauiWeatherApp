using Newtonsoft.Json;
using System.ComponentModel;

namespace WeatherApp.Logic.Models
{
    public class ForecastEntry : INotifyPropertyChanged
    {
        [JsonProperty("dt")]
        public int TimeForecastedUnixUTC { get; set; }

        public DateTime TimeForecastedDateTime => DateTimeOffset.FromUnixTimeSeconds(TimeForecastedUnixUTC).LocalDateTime;

        public string ForecastedDatetimeFormatted => TimeForecastedDateTime.ToString("dddd HH:mm");
        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        public string VisibilityInKM => $"Visibility: {Visibility / 1000}";

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("snow")]
        public Snow Snow { get; set; }

        public string Description => Weather is not null ? Weather.First().Description : string.Empty;

        public string IconSource => Weather is not null ? Weather.First().IconImage : string.Empty;

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if(_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
