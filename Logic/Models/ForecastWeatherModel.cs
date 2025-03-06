
namespace WeatherApp.Logic.Models
{
    public class ForecastWeatherModel
    {
            public string cod { get; set; }
            public int message { get; set; }
            public int cnt { get; set; }
            public List<ForecastEntry> list { get; set; }
            public City city { get; set; }
    }
}
