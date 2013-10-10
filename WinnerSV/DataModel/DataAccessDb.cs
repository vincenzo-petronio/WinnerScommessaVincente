using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLiteWinRT;
using Windows.Storage;
using WinnerSV.Common;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Definisce i metodi di accesso in lettura e scrittura al DB.
    /// </summary>
    public class DataAccessDb : IDataAccessDb
    {
        /// <summary>
        /// Costruttore.
        /// </summary>
        public DataAccessDb()
        {
            CreateDB();
        }

        /// <summary>
        /// Verifica se esiste, altrimenti crea un nuovo DB con una tabella per consentire il 
        /// salvataggio dell'oggetto da memorizzare.
        /// </summary>
        ////private async void TryToCreateDB()
        ////{
        ////    try
        ////    {
        ////        await ApplicationData.Current.LocalFolder.GetFileAsync(Constants.PATH_LOCAL_DB);
        ////    }
        ////    catch (FileNotFoundException)
        ////    {
        ////        CreateDB();
        ////    }
        ////}

        /// <summary>
        /// Restituisce la connessione al DB.
        /// </summary>
        /// <returns>SQLiteAsyncConnection</returns>
        private Database GetSQLiteConnection()
        {
            return new Database(ApplicationData.Current.LocalFolder, Constants.PATH_LOCAL_DB);
        }

#region CRUD

        /// <summary>
        /// Crea le Tabelle con le relazioni necessarie.
        /// </summary>
        private async void CreateDB()
        {
            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenOrCreateReadWrite);

                // TABLE SCHEDINA
                string querySqlCreateFirstTable =
                    " CREATE TABLE IF NOT EXISTS " + Constants.TABLE_NAME_SCHEDINA  +
                    " (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, "             +
                    " Title varchar(50) NOT NULL UNIQUE) ";
                await dbInstance.ExecuteStatementAsync(querySqlCreateFirstTable);

                // TABLE SCOMMESSA
                string querySqlCreateSecondTable =
                    " CREATE TABLE IF NOT EXISTS " + Constants.TABLE_NAME_SCOMMESSA     +
                    " (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, "                 +
                    " IdScommessa INTEGER NOT NULL, "                                   +
                    " Date VARCHAR(200), "                                              +
                    " TeamCasa VARCHAR(200), "                                          +
                    " TeamFCasa VARCHAR(200), "                                         +
                    " FOREIGN KEY(IdScommessa) REFERENCES SCHEDINA(Id) ON DELETE CASCADE) ";
                await dbInstance.ExecuteStatementAsync(querySqlCreateSecondTable);

                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - Create] \t" + "Table create con successo!");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - Create] \n" + e.ToString());
            }
        }

        /// <summary>
        /// Restituisce una lista completa di elementi presenti nel DB.
        /// </summary>
        /// <returns>List di oggetti Schedina</returns>
        public async Task<List<Schedina>> GetSchedine()
        {
            List<Schedina> schedine = new List<Schedina>();
            Database dbInstance = GetSQLiteConnection();
            await dbInstance.OpenAsync(SqliteOpenMode.OpenRead);
            
            // TODO

            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetSchedine] \t" + "GetSchedine: {0} records presenti nel DB", schedine.Count);
            return schedine;
        }

        /// <summary>
        /// Restituisce l'elemento specificato dal parametro, se presente nel DB.
        /// </summary>
        /// <param name="t">string </param>
        /// <returns>schedina</returns>
        public async Task<Schedina> GetSchedina(string t)
        {
            Database dbInstance = GetSQLiteConnection();
            await dbInstance.OpenAsync(SqliteOpenMode.OpenRead);

            // TODO

            return null;
        }

        /// <summary>
        /// Consente di salvare una nuova Schedina nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        public async Task<bool> SetSchedina(string title)
        {
            bool isCompleted = false;
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

                string query = " INSERT OR FAIL INTO " + Constants.TABLE_NAME_SCHEDINA + " (Title) VALUES (@Title) ";
                Statement statement = await dbInstance.PrepareStatementAsync(query);
                statement.BindTextParameterWithName("@Title", title);
                await statement.StepAsync();
                isCompleted = true;
                messageForLog = "Oggetto Schedina salvato nel DB!";
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                var result = SQLiteWinRT.Database.GetSqliteErrorCode(ex.HResult);
                //throw new Exception("[DATAACCESSDB - SetSchedina] \n" + result);
                isCompleted = false;
                messageForLog = result.ToString();
            }
            catch (Exception ex)
            {
                isCompleted = false;
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - SetSchedina] \n" + messageForLog + "\n");
            }

            return isCompleted;
        }

        /// <summary>
        /// Consente di aggiornare un elemento nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        /// <returns></returns>
        public async Task UpdateSchedina(Schedina s)
        {
            Database dbInstance = GetSQLiteConnection();
            await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

            // TODO

            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - UpdateSchedina] \t" + "Oggetto Schedina aggiornato nel DB!");
        }

        /// <summary>
        /// Elimina dal DB l'elemento indicato dal parametro.
        /// </summary>
        /// <param name="s">Schedina </param>
        public async Task DeleteSchedina(Schedina s)
        {
            Database dbInstance = GetSQLiteConnection();
            await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

            // TODO

            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - DeleteSchedina] \t" + "Oggetto Schedina cancellato dal DB!");
        }

#endregion

    }
}
