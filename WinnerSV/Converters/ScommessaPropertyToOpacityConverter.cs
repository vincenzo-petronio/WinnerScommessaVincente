using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WinnerSV.DataModel;

namespace WinnerSV.Converters
{
    /// <summary>
    /// Converter per eseguire la conversione da un parametro dell'oggetto Scommessa in una proprietà Opacity.
    /// Una quota non giocata corrisponde ad un pulsante attivo.
    /// Una quota già giocata corrisponde ad un pulsante opaco.
    /// </summary>
    public class ScommessaPropertyToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double opacity = 1;
            if (value != null)
            {
                Scommessa s = value as Scommessa;
                switch ((string)parameter)
                {
                    case "Q1":
                        {
                            // Se la string non è vuota, allora è una quota giocata, quindi imposta l'opacity.
                            if (!string.IsNullOrEmpty(s.Q1))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "Q1 opacity LOW");
                            }
                        }
                        break;
                    case "QX":
                        {
                            if (!string.IsNullOrEmpty(s.QX))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "QX opacity LOW");
                            }
                        }
                        break;
                    case "Q2":
                        {
                            if (!string.IsNullOrEmpty(s.Q2))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "Q2 opacity LOW");
                            }
                        }
                        break;
                    default: break;
                }
            }
            return opacity;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // One-Way conversion!
            return null;
        }
    }
}
