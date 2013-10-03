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

        /// <summary>
        /// Restituisce la connessione al DB.
        /// </summary>
        /// <returns>SQLiteAsyncConnection</returns>
        private SQLiteAsyncConnection GetSQLiteConnection()
        {
            return new SQLiteAsyncConnection(pathLocalDb, true);
        }

#region CRUD

        /// <summary>
        /// Crea la Tabella.
        /// </summary>
        private async void CreateDB()
        {
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
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
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
            var query = connAsync.Table<Schedina>();
            schedine = await query.ToListAsync();
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "GetSchedine: {0} records presenti nel DB", schedine.Count);
            return schedine;
        }

        /// <summary>
        /// Restituisce l'elemento specificato dal parametro, se presente nel DB.
        /// </summary>
        /// <param name="t">string </param>
        /// <returns>schedina</returns>
        public async Task<Schedina> GetSchedina(string t)
        {
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
            //var query = connAsync.Table<Schedina>();
            string sqlQuery = "SELECT * FROM Schedina WHERE Title = '" + t + "'";
            var sqlQueryResults = await connAsync.QueryAsync<Schedina>(sqlQuery);
            System.Diagnostics.Debug.WriteLineIf(sqlQueryResults != null, "[DATAACCESSDB] \t" + "Schedina trovata nel DB: " + sqlQueryResults.Count);
            if (sqlQueryResults.Count != 0)
            {
                return sqlQueryResults.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Consente di salvare un elemento nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        public async Task SetSchedina(Schedina s)
        {
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
            //var query = connAsync.Table<Schedina>();
            await connAsync.InsertAsync(s);
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "Oggetto Schedina salvato nel DB!");
        }

        /// <summary>
        /// Consente di aggiornare un elemento nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        /// <returns></returns>
        public async Task UpdateSchedina(Schedina s)
        {
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
            var sqlQueryResults = await connAsync.FindAsync<Schedina>(x => x.Title == s.Title);
            if(sqlQueryResults != null)
            {
                await connAsync.UpdateAsync(s);
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "Oggetto Schedina aggiornato nel DB!");
            }
        }

        /// <summary>
        /// Elimina dal DB l'elemento indicato dal parametro.
        /// </summary>
        /// <param name="s">Schedina </param>
        public async Task DeleteSchedina(Schedina s)
        {
            SQLiteAsyncConnection connAsync = GetSQLiteConnection();
            //var query = connAsync.Table<Schedina>();
            await connAsync.DeleteAsync(s);
            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB] \t" + "Oggetto Schedina cancellato dal DB!");
        }

#endregion

    }
}
