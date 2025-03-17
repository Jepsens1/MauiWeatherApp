namespace WeatherApp.Logic
{
    public static class Utilities
    {
        /// <summary>
        /// Converts a int value withing 0-360 range to a string compass direction
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static string ConvertCompassDegToStringDirection(int deg)
        {
            string[] directions = ["North", "North East", "East", "South East", "South", "South West", "West", "North West"];

            int index = (int)Math.Round(deg / 45.0) % 8;

            return directions[index];
        }
    }
}
