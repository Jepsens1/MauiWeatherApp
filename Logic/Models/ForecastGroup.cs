namespace WeatherApp.Logic.Models
{
    public class ForecastGroup : List<ForecastEntry>
    {
        public string Date { get; private set; } = string.Empty;

        public ForecastGroup(string date, IEnumerable<ForecastEntry> forecasts) : base(forecasts)
        {
            Date = date;
        }
    }
}
