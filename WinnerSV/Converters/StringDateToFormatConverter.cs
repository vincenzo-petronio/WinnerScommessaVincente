using System;
using System.Windows.Data;

namespace WinnerSV.Converters
{
    /// <summary>
    /// Converter per eseguire la conversione da una stringa in un formato Data desiderato.
    /// </summary>
    public class StringDateToFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ( value != null && value.GetType() == typeof(string))
            {
                try
                {
                    // yyyy-MM-ddTHH:mm:ss
                    string dateBefore = (string)value;
                    // http://msdn.microsoft.com/it-it/library/az4se3k1(v=vs.110).aspx#GeneralDateShortTime
                    string dateAfter = DateTime.Parse(dateBefore, culture).ToString("g"); ////ToString(@"MM/dd/yyyy hh:mm");
                    System.Diagnostics.Debug.WriteLine("[StringDateToFormatConverter] \t" + 
                        "DataBefore: " + dateBefore + " - " +
                        "DataAfter: " + dateAfter + " - " +
                        "Culture: " + culture.Name.ToString());
                    return dateAfter;
                }
                catch(FormatException fe)
                {
                    System.Diagnostics.Debug.WriteLine("[StringDateToFormatConverter] \t" + fe.Message);
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // ONE-WAY Conversion!
            return null;
        }
    }
}
