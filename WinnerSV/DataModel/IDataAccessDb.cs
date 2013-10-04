using System.Collections.Generic;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Specifica i metodi di accesso in lettura e scrittura al DB.
    /// </summary>
    public interface IDataAccessDb
    {
        Task<List<Schedina>> GetSchedine();
        Task<Schedina> GetSchedina(string t);
        Task SetSchedina(Schedina s);
        Task DeleteSchedina(Schedina s);
    }
}
