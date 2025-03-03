namespace WeatherApp.Logic
{
    public static class Utilities
    {
        public static string ConvertCompassDegToStringDirection(int deg)
        {
            return deg switch
            {
                0 => "North",
                45 => "North East",
                90 => "East",
                135 => "South East",
                180 => "South",
                225 => "South West",
                270 => "West",
                315 => "North West",
                _ => "Unable to determine direction"
            };
        }
    }
}
