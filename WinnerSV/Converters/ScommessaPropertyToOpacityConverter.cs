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
                    case "HC1":
                        {
                            if (!string.IsNullOrEmpty(s.HC1))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "HC1 opacity LOW");
                            }
                        }
                        break;
                    case "HCX":
                        {
                            if (!string.IsNullOrEmpty(s.HCX))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "HCX opacity LOW");
                            }
                        }
                        break;
                    case "HC2":
                        {
                            if (!string.IsNullOrEmpty(s.HC2))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "HC2 opacity LOW");
                            }
                        }
                        break;
                    case "OVER":
                        {
                            if (!string.IsNullOrEmpty(s.OVER))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "OVER opacity LOW");
                            }
                        }
                        break;
                    case "UNDER":
                        {
                            if (!string.IsNullOrEmpty(s.UNDER))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "UNDER opacity LOW");
                            }
                        }
                        break;
                    case "DC1X":
                        {
                            if (!string.IsNullOrEmpty(s.DC1X))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "DC1X opacity LOW");
                            }
                        }
                        break;
                    case "DC12":
                        {
                            if (!string.IsNullOrEmpty(s.DC12))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "DC12 opacity LOW");
                            }
                        }
                        break;
                    case "DCX2":
                        {
                            if (!string.IsNullOrEmpty(s.DCX2))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "DCX2 opacity LOW");
                            }
                        }
                        break;
                    case "Home12":
                        {
                            if (!string.IsNullOrEmpty(s.Home12))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "Home12 opacity LOW");
                            }
                        }
                        break;
                    case "Away12":
                        {
                            if (!string.IsNullOrEmpty(s.Away12))
                            {
                                opacity = 0.3;
                                System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToOpacityConverter] \t" + "Away12 opacity LOW");
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
