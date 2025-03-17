namespace WeatherApp.Logic.Models
{
    public class ForecastDisplayModel
    {
        public List<ForecastGroup> GroupedData { get; set; } = new();

        //Add more fields in future when expanding
    }
}
