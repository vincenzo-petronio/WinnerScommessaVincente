//-----------------------------------------------------------------------
// <copyright file="IDataAccessDb.cs">
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
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Specifica le operazioni CRUD sul DB.
    /// </summary>
    public interface IDataAccessDb
    {
        Task<List<Schedina>> GetSchedine();
        Task<Schedina> GetSchedina(string t);
        Task<bool> SetSchedina(string s);
        Task<bool> DeleteSchedina(int id);
        Task<List<Scommessa>> GetScommesse(string t);
        Task<bool> UpdateScommessa(Incontro i, Schedina s);
        Task<bool> DeleteScommessa(Scommessa s);
        Task<List<Incontro>> GetIncontri(string t);
    }
}
