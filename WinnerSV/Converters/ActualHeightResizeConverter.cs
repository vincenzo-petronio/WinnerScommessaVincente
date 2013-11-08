﻿//-----------------------------------------------------------------------
// <copyright file="ActualHeightResizeConverter.cs">
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

using System;
using System.Windows;
using System.Windows.Data;

namespace WinnerSV.Converters
{
    /// <summary>
    /// Converter per eseguire la conversione da una dimensione in una versione in percentuale.
    /// </summary>
    public class ActualHeightResizeConverter : IValueConverter
    {
        // Percentuale che la griglia dell'anteprima deve occupare
        // rispetto al LayoutRoot.
        private double fullVisibleFactor = (5.5 * 0.1);
        // Percentuale che la griglia dell'anteprima deve traslare oltre
        // il bordo inferiore dello schermo.
        private double partiallyVisibleFactor = (4.5 * 0.1);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)parameter == "1")
            {
                return (value != null) ? ((double)value * fullVisibleFactor) : value;
            }
            else if ((string)parameter == "2")
            {
                return (value != null) ? ((double)value * partiallyVisibleFactor) : value;
            }
            else if ((string)parameter == "3")
            {
                // Bottom = Percentuale visibile a valle della traslazione
                double diffVisibleFactor = fullVisibleFactor - partiallyVisibleFactor;
                return new Thickness(0, 0, 0, (double)value * diffVisibleFactor);
            }
            else if ((string)parameter == "4")
            {
                // Top = (percentuale di griglia visibile a valle della traslazione) + 5px
                // Left,Right,Bottom = 5px
                double diffVisibleFactor = fullVisibleFactor - partiallyVisibleFactor;
                return new Thickness(5, (double)value * diffVisibleFactor + 5, 5, 5);
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
