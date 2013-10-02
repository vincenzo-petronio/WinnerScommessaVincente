using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WinnerSV.Common;
using WinnerSV.DataModel;

namespace WinnerSV.DataSample
{
    /// <summary>
    /// Fornisce dati fittizi per popolare il DB.
    /// </summary>
    public class DbData
    {
        /// <summary>
        /// Costruttore.
        /// </summary>
        public DbData()
        {
        }

        public async Task PopolaDb()
        {
            SQLiteAsyncConnection connAsync = new SQLiteAsyncConnection(
                Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.PATH_LOCAL_DB), 
                true);

            var query = connAsync.Table<Schedina>();
            var results = await query.ToListAsync();
            if (results.Count == 0)
            {
                Random rNumber = new Random();

                for (int i = 0; i < 5; i++)
                {
                    Schedina s = new Schedina
                    {
                        Title = "Nome Cognome " + rNumber.Next(100, 150),
                    };
                    await connAsync.InsertAsync(s);
                }
                System.Diagnostics.Debug.WriteLine("[DBDATA] \t" + "5 record fittizi inseriti nel DB!");
            }
        }
    }
}
