namespace WeatherApp.Logic.Models
{
    public class ForecastEntry
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        //public Sys sys { get; set; }
        public string dt_txt { get; set; }
        public Rain rain { get; set; }
    }
}
