//-----------------------------------------------------------------------
// <copyright file="Constants.cs">
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
using System.IO.IsolatedStorage;

namespace WinnerSV.Common
{
    /// <summary>
    /// Costanti hard-coded.
    /// </summary>
    public class Constants
    {
        // ENDPOINT 
        public const string URL_DOMAIN_WINNER = "http://winnerscommessavincente.altervista.org/";
        public const string URL_SUBDOMAIN_JSON = URL_DOMAIN_WINNER + "Json/";
        public const string URL_JSON = "_scommesse.json";

        // DB
        public const string PATH_LOCAL_DB = "db_schedine.sqlite";
        public const string TABLE_NAME_SCHEDINA = "SCHEDINA";
        public const string TABLE_NAME_SCOMMESSA = "SCOMMESSA";

        // SETTINGS
        public const string SETTINGS_KEY_CORRENTE_LINGUA = "lingua_corrente";

        // DATI
        public const string TITLE_SCHEDINA_DEFAULT = "Schedina";
    }
}
