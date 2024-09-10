using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace LiveTune.Converters
{
    public class MillisecondToTimeStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is long millisecond)
            {
                var time = TimeSpan.FromMilliseconds(millisecond);
                return time.ToString(@"hh\:mm\:ss");
            }
            return "00:00:00";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.TotalMilliseconds;
            }
            return 0;
        }
    }
}
