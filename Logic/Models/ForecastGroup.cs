namespace WeatherApp.Logic.Models
{
    /// <summary>
    /// Used for grouping ForecastEntry into a single group based on date
    /// </summary>
    public class ForecastGroup : List<ForecastEntry>
    {
        public string Date { get; private set; } = string.Empty;

        public ForecastGroup(string date, IEnumerable<ForecastEntry> forecasts) : base(forecasts)
        {
            Date = date;
        }
    }
}
