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
        Task<List<Incontro>> GetIncontri(string t);
    }
}
