using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Specifica i metodi di accesso in lettura e scrittura al DB.
    /// </summary>
    public interface IDataAccessDb
    {
        Task<List<Schedina>> GetSchedine();
    }
}
