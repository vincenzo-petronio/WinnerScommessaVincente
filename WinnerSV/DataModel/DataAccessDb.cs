using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLiteWinRT;
using Windows.Storage;
using WinnerSV.Common;
using System.Text;

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
        /// Restituisce la connessione al DB.
        /// </summary>
        /// <returns>Database</returns>
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
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenOrCreateReadWrite);

                // TABLE SCHEDINA
                string querySqlCreateFirstTable =
                    " CREATE TABLE IF NOT EXISTS " + Constants.TABLE_NAME_SCHEDINA +
                    " ( " + 
                    " Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    " Title varchar(50) NOT NULL UNIQUE " + 
                    " ) ";
                await dbInstance.ExecuteStatementAsync(querySqlCreateFirstTable);

                // TABLE SCOMMESSA
                string querySqlCreateSecondTable =
                    " CREATE TABLE IF NOT EXISTS " + Constants.TABLE_NAME_SCOMMESSA +
                    " ( " +
                    ////" Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    " IdScommessa INTEGER, " +
                    " TeamCasa VARCHAR(200) NOT NULL, " +
                    " TeamFCasa VARCHAR(200) NOT NULL, " +
                    " Data VARCHAR(200) NOT NULL, " +
                    " IdMatch VARCHAR(200) NOT NULL, " +
                    " TotalScore VARCHAR(200), " +
                    " Over VARCHAR(200), " +
                    " Under VARCHAR(200), " +
                    " Q1 VARCHAR(200), " +
                    " Qx VARCHAR(200), " +
                    " Q2 VARCHAR(200), " +
                    " Hc VARCHAR(200), " +
                    " Hc1 VARCHAR(200), " +
                    " Hc2 VARCHAR(200), " +
                    " Hcx VARCHAR(200), " +
                    " Dc1x VARCHAR(200), " +
                    " Dc12 VARCHAR(200), " +
                    " Dcx2 VARCHAR(200), " +
                    " Home12 VARCHAR(200), " +
                    " Away12 VARCHAR(200), " +
                    " PRIMARY KEY(IdScommessa, TeamCasa, TeamFCasa, Data, IdMatch), " +
                    " FOREIGN KEY(IdScommessa) REFERENCES SCHEDINA(Id) ON DELETE CASCADE " +
                    " ) ";
                await dbInstance.ExecuteStatementAsync(querySqlCreateSecondTable);

                string querySqlEnablePragma = @"PRAGMA foreign_keys = ON";
                await dbInstance.ExecuteStatementAsync(querySqlEnablePragma);

                messageForLog = "Query eseguita con successo";
            }
            catch (Exception e)
            {
                messageForLog = e.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - CreateDB] \t" + messageForLog);
            }
        }

        /// <summary>
        /// Restituisce una lista completa di elementi Schedina presenti nel DB.
        /// </summary>
        /// <returns>List di oggetti Schedina</returns>
        public async Task<List<Schedina>> GetSchedine()
        {
            List<Schedina> schedine = new List<Schedina>();
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenRead);

                string query = " SELECT * FROM " + Constants.TABLE_NAME_SCHEDINA;
                using (Statement statement = await dbInstance.PrepareStatementAsync(query))
                {
                    while (await statement.StepAsync())
                    {
                        schedine.Add(new Schedina { Id = statement.GetIntAt(0), Title = statement.GetTextAt(1) });
                    }
                    messageForLog = "Query eseguita con successo";
                }

#if DEBUG
                string query1 = " SELECT * FROM " + Constants.TABLE_NAME_SCHEDINA;
                using (Statement statement = await dbInstance.PrepareStatementAsync(query1))
                {
                    StringBuilder sb = new StringBuilder();
                    while (await statement.StepAsync())
                    {
                        sb.AppendLine(" | " + statement.GetIntAt(0).ToString() + " | " + statement.GetTextAt(1) + " | ");
                    }
                    sb.AppendLine("Schedine presenti: " + schedine.Count.ToString());
                    System.Diagnostics.Debug.WriteLine(sb.ToString());
                }

                string query2 = " SELECT * FROM " + Constants.TABLE_NAME_SCOMMESSA;
                using (Statement statement = await dbInstance.PrepareStatementAsync(query2))
                {
                    StringBuilder sb2 = new StringBuilder();
                    while (await statement.StepAsync())
                    {
                        sb2.Append(" | " + statement.GetIntAt(0).ToString());
                        for (int i = 1; i <= 20; i++)
                        {
                            sb2.Append(" | " + statement.GetTextAt(i));
                        }
                        sb2.Append("\n");
                    }
                    System.Diagnostics.Debug.WriteLine(sb2.ToString());
                }
#endif
            }
            catch (Exception ex)
            {
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetSchedine] \t" + messageForLog);
            }

            return schedine;
        }

        /// <summary>
        /// Restituisce l'elemento Schedina specificato dal parametro, se presente nel DB.
        /// </summary>
        /// <param name="t">string titolo della schedina</param>
        /// <returns>Schedina</returns>
        public async Task<Schedina> GetSchedina(string t)
        {
            Schedina s = new Schedina();
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenRead);

                string query = " SELECT * FROM " + Constants.TABLE_NAME_SCHEDINA + " WHERE Title=@Title";
                using (Statement statement = await dbInstance.PrepareStatementAsync(query))
                {
                    statement.BindTextParameterWithName("@Title", t);
                    if (await statement.StepAsync())
                    {
                        s.Id = statement.GetIntAt(0);
                        s.Title = statement.GetTextAt(1);
                        messageForLog = "Schedina trovata: Id = " + s.Id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetSchedina] \t" + messageForLog);
            }

            return s;
        }

        /// <summary>
        /// Consente di salvare una nuova Schedina nel DB.
        /// </summary>
        /// <param name="s">String titolo della Schedina</param>
        public async Task<bool> SetSchedina(string title)
        {
            bool isCompleted = false;
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

                string query = " INSERT OR IGNORE INTO " + Constants.TABLE_NAME_SCHEDINA + " (Title) VALUES (@Title) ";
                using (Statement statement = await dbInstance.PrepareStatementAsync(query))
                {
                    statement.BindTextParameterWithName("@Title", title);
                    await statement.StepAsync();
                    isCompleted = true;
                    messageForLog = "Query eseguita con successo";
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                var result = SQLiteWinRT.Database.GetSqliteErrorCode(ex.HResult);
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
        /// Consente di aggiornare un elemento Schedina presente nel DB.
        /// </summary>
        /// <param name="s">Schedina</param>
        public async Task UpdateSchedina(Schedina s)
        {
            Database dbInstance = GetSQLiteConnection();
            await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

            // TODO

            System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - UpdateSchedina] \t" + "Oggetto Schedina aggiornato nel DB!");
        }

        /// <summary>
        /// Elimina dal DB l'elemento Schedina indicato dal parametro.
        /// </summary>
        /// <param name="t">String titolo della Schedina</param>
        public async Task<bool> DeleteSchedina(int id)
        {
            string messageForLog = string.Empty;
            bool isCompleted = false;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

                string query = " DELETE FROM " + Constants.TABLE_NAME_SCHEDINA +
                    " WHERE Id=" + id;
                await dbInstance.ExecuteStatementAsync(query);
                messageForLog = "Query eseguita con successo";
                isCompleted = true;
            }
            catch (Exception ex)
            {
                isCompleted = false;
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - DeleteSchedina] \t" + messageForLog.ToString());
            }

            return isCompleted;
        }

        /// <summary>
        /// Restituisce un elenco di scommesse con relative quote giocate, 
        /// per una determinata Schedina.
        /// </summary>
        /// <returns>List di oggetti Scommessa</returns>
        /// <param name="t">String Titolo della Schedina contenente le scommesse</param>
        public async Task<List<Scommessa>> GetScommesse(string t)
        {
            List<Scommessa> scommesse = new List<Scommessa>();
            string messageForLog = string.Empty;

            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenRead);

                // Query per selezionare tutti gli incontri nella tabella Scommessa la cui chiave esterna 
                // coincide nella tabella Schedina con l'Id della Schedina indicata dal titolo.
                string query = " SELECT * FROM " + Constants.TABLE_NAME_SCOMMESSA +
                    " INNER JOIN " + Constants.TABLE_NAME_SCHEDINA +
                    " ON " + Constants.TABLE_NAME_SCOMMESSA + ".IdScommessa" +
                    " = " +
                    Constants.TABLE_NAME_SCHEDINA + ".Id" +
                    " WHERE " +
                    Constants.TABLE_NAME_SCHEDINA + ".Title" +
                    " = '" +
                    t + "'";
                using (Statement statement = await dbInstance.PrepareStatementAsync(query))
                {
                    statement.EnableColumnsProperty();
                    System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetIncontri] \t" + query.ToString());
                    while (await statement.StepAsync())
                    {
                        var columns = statement.Columns;
                        scommesse.Add(new Scommessa
                        {
                            IdScommessa = int.Parse(columns["IdScommessa"]),
                            TeamCasa = columns["TeamCasa"],
                            TeamFCasa = columns["TeamFCasa"],
                            Data = columns["Data"],
                            IdMatch = columns["IdMatch"],
                            TotalScore = columns["TotalScore"],
                            OVER = columns["Over"],
                            UNDER = columns["Under"],
                            Q1 = columns["Q1"],
                            QX = columns["Qx"],
                            Q2 = columns["Q2"],
                            HC = columns["Hc"],
                            HC1 = columns["Hc1"],
                            HC2 = columns["Hc2"],
                            HCX = columns["Hcx"],
                            DC1X = columns["Dc1x"],
                            DC12 = columns["Dc12"],
                            DCX2 = columns["Dcx2"],
                            Home12 = columns["Home12"],
                            Away12 = columns["Away12"]
                        });
                    }
                    messageForLog = "Query eseguita con successo";
                }
            }
            catch (Exception e)
            {
                messageForLog = e.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetScommesse] \t" + messageForLog);
            }

            return scommesse;
        }

        /// <summary>
        /// Esegue l'Update di una scommessa nel DB.
        /// </summary>
        /// <param name="i">Incontro Oggetto con i dati dell'incontro con le quote della scommessa effettuata</param>
        /// <param name="s">Schedina Oggetto con i dati della schedina in corso di modifica.</param>
        /// <returns>bool False se ci sono eccezioni, True altrimenti.</returns>
        public async Task<bool> UpdateScommessa(Incontro i, Schedina s)
        {
            bool isUpdated = false;
            string messageForLog = string.Empty;
            
            try
            {
                Database dbInstance = GetSQLiteConnection();
                await dbInstance.OpenAsync(SqliteOpenMode.OpenReadWrite);

                // Una scommessa e' composta da piu' incontri, dove per ogni incontro e' possibile
                // giocare diverse quote.

                // Query per inserire i dati dell'incontro, se non presente. Ogni incontro e' identificato
                // da una serie di chiavi primarie.
                string querySql1 = " INSERT OR IGNORE INTO " + Constants.TABLE_NAME_SCOMMESSA +
                    " (IdScommessa, TeamCasa, TeamFCasa, Data, IdMatch) " +
                    " VALUES (@IdScommessa, @TeamCasa, @TeamFCasa, @Data, @IdMatch) ";
                using (Statement statement = await dbInstance.PrepareStatementAsync(querySql1))
                {
                    statement.BindIntParameterWithName("@IdScommessa", s.Id);
                    statement.BindTextParameterWithName("@TeamCasa", i.TeamCasa);
                    statement.BindTextParameterWithName("@TeamFCasa", i.TeamFCasa);
                    statement.BindTextParameterWithName("@Data", i.Data);
                    statement.BindTextParameterWithName("@IdMatch", i.IdMatch);
                    await statement.StepAsync();
                }

                // Query per inserire le quote per un determinato incontro. Se ho valori, effettuo l'update, altrimenti lascio
                // il valore presente.
                string querySql2 = " UPDATE " + Constants.TABLE_NAME_SCOMMESSA +
                    " SET " +
                    " TotalScore = case when coalesce('" + i.TotalScore + "', '') = '' then TotalScore else '" + i.TotalScore + "' end, " +
                    " Over = case when coalesce('" + i.OVER + "', '') = '' then Over else '" + i.OVER + "' end, " +
                    " Under = case when coalesce('" + i.UNDER + "', '') = '' then Under else '" + i.UNDER + "' end, " +
                    " Q1 = case when coalesce('" + i.Q1 + "', '') = '' then Q1 else '" + i.Q1 + "' end, " +
                    " Qx = case when coalesce('" + i.QX + "', '') = '' then Qx else '" + i.QX + "' end, " +
                    " Q2 = case when coalesce('" + i.Q2 + "', '') = '' then Q2 else '" + i.Q2 + "' end, " +
                    " Hc = case when coalesce('" + i.HC + "', '') = '' then Hc else '" + i.HC + "' end, " +
                    " Hc1 = case when coalesce('" + i.HC1 + "', '') = '' then Hc1 else '" + i.HC1 + "' end, " +
                    " Hc2 = case when coalesce('" + i.HC2 + "', '') = '' then Hc2 else '" + i.HC2 + "' end, " +
                    " Hcx = case when coalesce('" + i.HCX + "', '') = '' then Hcx else '" + i.HCX + "' end, " +
                    " Dc1x = case when coalesce('" + i.DC1X + "', '') = '' then Dc1x else '" + i.DC1X + "' end, " +
                    " Dc12 = case when coalesce('" + i.DC12 + "', '') = '' then Dc12 else '" + i.DC12 + "' end, " +
                    " Dcx2 = case when coalesce('" + i.DCX2 + "', '') = '' then Dcx2 else '" + i.DCX2 + "' end, " +
                    " Home12 = case when coalesce('" + i.Home12 + "', '') = '' then Home12 else '" + i.Home12 + "' end, " +
                    " Away12 = case when coalesce('" + i.Away12 + "', '') = '' then Away12 else '" + i.Away12 + "' end " +
                    " WHERE " +
                    " IdScommessa = '" + s.Id + "'" +
                    " AND " +
                    " TeamCasa = '" + i.TeamCasa + "'" +
                    " AND " +
                    " TeamFCasa = '" + i.TeamFCasa + "'" +
                    " AND " +
                    " Data = '" + i.Data + "'" +
                    " AND " +
                    " IdMatch = '" + i.IdMatch + "'";
                using (Statement statement = await dbInstance.PrepareStatementAsync(querySql2))
                {
                    System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - UpdateScommessa] \t" + querySql2.ToString());
                    await statement.StepAsync();
                    isUpdated = true;
                    messageForLog = "Query eseguita con successo";
                }
            }
            catch (Exception ex)
            {
                isUpdated = false;
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - UpdateScommessa] \n" + messageForLog + "\n");
            }
            
            return isUpdated;
        }

        /// <summary>
        /// Restituisce un elenco di scommesse con relative quote giocate, 
        /// per una determinata Schedina.
        /// </summary>
        /// <param name="title">String Titolo della schedina contenente le scommesse</param>
        /// <returns>List di oggetto Scommessa</returns>
        public async Task<List<Incontro>> GetIncontri(string title)
        {
            List<Incontro> incontri = new List<Incontro>();
            string messageForLog = string.Empty;

            try
            {
                
            }
            catch(Exception ex)
            {
                messageForLog = ex.Message;
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("[DATAACCESSDB - GetIncontri] \t" + messageForLog);
            }
            return incontri;
        }

#endregion

    }
}
