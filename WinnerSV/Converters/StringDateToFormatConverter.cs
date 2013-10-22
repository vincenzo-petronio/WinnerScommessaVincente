//-----------------------------------------------------------------------
// <copyright file="StringDateToFormatConverter.cs">
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
