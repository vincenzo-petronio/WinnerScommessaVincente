using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WinnerSV.Common;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Definisce i metodi di accesso in lettura e scrittura al DB.
    /// </summary>
    public class DataAccessDb : IDataAccessDb
    {
        private string pathLocalDb = Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.PATH_LOCAL_DB);
        
        /// <summary>
        /// Costruttore.
        /// </summary>
        public DataAccessDb()
        {
            CreateDB();
        }

        /// <summary>
        /// Creazione di un nuovo DB con una tabella per consentire il salvataggio dell'oggetto
        /// da memorizzare.
        /// </summary>
        private async void CreateDB()
        {
            SQLiteAsyncConnection connAsync = new SQLiteAsyncConnection(
                pathLocalDb, 
                true);
            await connAsync.CreateTableAsync<Schedina>();
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "Table SCHEDINA creata con successo.");
        }

        /// <summary>
        /// Restituisce una lista completa di elementi presenti nel DB.
        /// </summary>
        /// <returns>List di oggetti Schedina</returns>
        public async Task<List<Schedina>> GetSchedine()
        {
            List<Schedina> schedine = new List<Schedina>();
            SQLiteAsyncConnection connAsync = new SQLiteAsyncConnection(pathLocalDb, true);
            var query = connAsync.Table<Schedina>();
            schedine = await query.ToListAsync();
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "GetSchedine: {0} records presenti nel DB", schedine.Count);
            return schedine;
        }
    }
}
