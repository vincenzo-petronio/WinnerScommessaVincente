//-----------------------------------------------------------------------
// <copyright file="Scommessa.cs">
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
      
namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare un elemento della tabella Scommessa nel DB.
    /// Ogni Scommessa contiene gli stessi elementi dell'oggetto Incontro piu' 
    /// un identificativo.
    /// </summary>
    public class Scommessa
    {
        public int IdScommessa { get; set; } // FOREIGN KEY
        public string TeamCasa { get; set; }
        public string TeamFCasa { get; set; }
        public string Data { get; set; }
        public string IdMatch { get; set; }
        public string TotalScore { get; set; }
        public string OVER { get; set; }
        public string UNDER { get; set; }
        public string Q1 { get; set; }
        public string QX { get; set; }
        public string Q2 { get; set; }
        public string HC { get; set; }
        public string HC1 { get; set; }
        public string HC2 { get; set; }
        public string HCX { get; set; }
        public string DC1X { get; set; }
        public string DC12 { get; set; }
        public string DCX2 { get; set; }
        public string Home12 { get; set; }
        public string Away12 { get; set; }
    }
}
