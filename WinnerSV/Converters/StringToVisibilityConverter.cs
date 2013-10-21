using System;
using System.Windows;
using System.Windows.Data;

namespace WinnerSV.Converters
{
    /// <summary>
    /// Converter per eseguire la conversione da una stringa in una proprieta' Visibility.
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // ONE-WAY Conversion!
            return null;
        }
    }
}
