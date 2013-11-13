//-----------------------------------------------------------------------
// <copyright file="Sports.cs">
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

using System.Collections.Generic;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare tutti i sport seguendo la struttra del file json.
    /// </summary>
    public class Sports
    {
        public List<Calcio> Calcio { get; set; }
        public List<Tennis> Tennis { get; set; }
        public List<Basket> Basket { get; set; }
    }
    
    /// <summary>
    /// Contiene tutti i dati di un incontro sportivo
    /// </summary>
    public class Incontro
    {
        public string TeamCasa { get; set; }
        public string TeamFCasa { get; set; }

        private string data;
        public string Data 
        { 
            get 
            { 
                return data; 
            } 
            
            set 
            {
                // M/d/yyyy h:mm tt
                data = System.DateTime.Parse(value).ToString("g"); 
            } 
        }

        public string Q1 { get; set; }
        public string QX { get; set; }
        public string Q2 { get; set; }
        public string HC { get; set; }
        public string HC1 { get; set; }
        public string HCX { get; set; }
        public string HC2 { get; set; }
        public string TotalScore { get; set; }
        public string OVER { get; set; }
        public string UNDER { get; set; }
        public string DC1X { get; set; }
        public string DCX2 { get; set; }
        public string DC12 { get; set; }
        public string IdMatch { get; set; }
        public string Home12 { get; set; }
        public string Away12 { get; set; }
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Calcio
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriCalcio { get; set; }
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Tennis
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriTennis { get; set; }
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Basket
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriBasket { get; set; }
    }

}
