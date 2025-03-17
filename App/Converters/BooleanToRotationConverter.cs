using System.Globalization;

namespace App.Converters
{
    public class BooleanToRotationConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? 90 : 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (double)value == 90;
        }
    }
}
