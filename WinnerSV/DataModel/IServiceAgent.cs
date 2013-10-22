//-----------------------------------------------------------------------
// <copyright file="IServiceAgent.cs">
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

namespace WinnerSV.DataModel
{
    /// <summary>
    /// interfaccia 
    /// </summary>
    public interface IServiceAgent
    {
        void GetSports(Action<Sports, Exception> callback, string uri);
    }
}
