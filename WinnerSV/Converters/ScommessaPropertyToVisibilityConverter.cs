//-----------------------------------------------------------------------
// <copyright file="ScommessaPropertyToVisibilityConverter.cs">
//    Copyright (c) 2013 Vincenzo Petronio <vincenzo.petronio"AT"gmail.com>. All rights reserved.
// </copyright>
// <license>
//    This file is part of Winner - Scommessa vincente.
//
//    Winner - Scommessa vincente is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 2 of the License, or
//    (at your option) any later version.
//
//    Winner - Scommessa vincente is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Winner - Scommessa vincente.  If not, see <http://www.gnu.org/licenses/>.
// </license>
//-----------------------------------------------------------------------

using System.Windows;
using System.Windows.Data;
using WinnerSV.DataModel;

namespace WinnerSV.Converters
{
    /// <summary>
    /// Converter per eseguire la conversione da una proprietà dell'oggetto Scommessa
    /// in una proprieta' Visibility.
    /// </summary>
    public class ScommessaPropertyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Scommessa s = value as Scommessa;
                // 1 X 2
                if (((string)parameter) == "1X2" && (!string.IsNullOrEmpty(s.Q1) || !string.IsNullOrEmpty(s.QX) || !string.IsNullOrEmpty(s.Q2)))
                {
                    System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa 1X2 Visible");
                    return Visibility.Visible;
                }
                // Over/Hunder
                if((string)parameter == "OverUnder" && !string.IsNullOrEmpty(s.TotalScore))
                {
                    System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa Over/Under Visible");
                    return Visibility.Visible;
                }
                // Handicap
                if((string)parameter == "Handicap" && !string.IsNullOrEmpty(s.HC))
                {
                    System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa Handicap Visible");
                    return Visibility.Visible;
                }
                // Doppia Chance
                if ((string)parameter == "DoppiaChance" && (!string.IsNullOrEmpty(s.DC12) || !string.IsNullOrEmpty(s.DC1X) || !string.IsNullOrEmpty(s.DCX2)))
                {
                    System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa DoppiaChance Visible");
                    return Visibility.Visible;
                }
                // Home Away
                if((string)parameter == "HomeAway" && (!string.IsNullOrEmpty(s.Home12) || !string.IsNullOrEmpty(s.Away12)))
                {
                    System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa HomeAway Visible");
                    return Visibility.Visible;
                }
            }
            System.Diagnostics.Debug.WriteLine("[ScommessaPropertyToVisibilityConverter] \t" + "Scommessa Collapsed");
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // One-Way Conversion!
            return null;
        }
    }
}
