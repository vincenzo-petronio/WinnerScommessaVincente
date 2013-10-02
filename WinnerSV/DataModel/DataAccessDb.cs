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
            TryToCreateDB();
        }

        /// <summary>
        /// Verifica se esiste, altrimenti crea un nuovo DB con una tabella per consentire il 
        /// salvataggio dell'oggetto da memorizzare.
        /// </summary>
        private async void TryToCreateDB()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(Constants.PATH_LOCAL_DB);
            }
            catch (FileNotFoundException)
            {
                CreateDB();
            }
        }

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

        /// <summary>
        /// Consente di salvare un oggeto nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        /// <returns></returns>
        public async Task SetSchedina(Schedina s)
        {
            SQLiteAsyncConnection connAsync = new SQLiteAsyncConnection(pathLocalDb, true);
            var query = connAsync.Table<Schedina>();
            await connAsync.InsertAsync(s);
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "Oggetto Schedina salvato nel DB!");
        }
    }
}
