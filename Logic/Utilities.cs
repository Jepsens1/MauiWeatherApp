namespace WeatherApp.Logic
{
    public static class Utilities
    {
        public static string ConvertCompassDegToStringDirection(int deg)
        {
            string[] directions = ["North", "North East", "East", "South East", "South", "South West", "West", "North West"];

            int index = (int)Math.Round(deg / 45.0) % 8;

            return directions[index];
        }
    }
}
