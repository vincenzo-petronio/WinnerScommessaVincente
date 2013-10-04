using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Threading.Tasks;
using WinnerSV.Common;
using WinnerSV.Helpers;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Definisce i metodi per accedere al WebServices.
    /// </summary>
    public class ServiceAgentSports : IServiceAgent
    {
        private string currentLanguage = string.Empty;
        private string url = string.Empty;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public ServiceAgentSports()
        {
        }

        /// <summary>
        /// Ricava dai settings la lingua di sistema.
        /// </summary>
        private void RilevaLingua()
        {
            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                string lingua = "it";
                if (settings.Contains(Constants.SETTINGS_KEY_CORRENTE_LINGUA))
                {
                    // La lingua memorizzata e' nel formato BCP-47.
                    // en-US diventa en.
                    lingua = ((string)settings[Constants.SETTINGS_KEY_CORRENTE_LINGUA]).Split('-')[0];
                }
                currentLanguage = lingua;
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \t" + "currentLanguage = " + currentLanguage);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \n" + e.ToString());
            }
        }

        /// <summary>
        /// Consente di scaricare un file Json da URL remoto.
        /// </summary>
        /// <param name="uri">string Path finale del file Json</param>
        /// <returns>string Contenuto del Json</returns>
        private async Task<string> DownloadFileJson(string uri)
        {
            RilevaLingua();
            var downloaded = string.Empty;
            this.url = Constants.URL_SUBDOMAIN_JSON + this.currentLanguage + uri;
            if (!string.IsNullOrEmpty(this.url))
            {
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - URI] \t" + this.url);
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    HttpWebRequest client = (HttpWebRequest)WebRequest.Create(this.url);
                    client.Method = "Get";
                    HttpWebResponse response = (HttpWebResponse)await client.GetResponseAsync();
                    sw.Stop();

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("NotFound");
                    }
                    else
                    {
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            downloaded = System.Net.WebUtility.HtmlDecode(sr.ReadToEnd());
                            System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - DownloadFileJson] \t" 
                                + "Download JSON terminato in {0} s", sw.Elapsed.TotalSeconds);
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - DownloadFileJson] \n" + ex.ToString());
                }
            }
            return downloaded;
        }

        /// <summary>
        /// Restituisce una oggetto Sports con i dati ottenuto dal JSON.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="uri">string Path finale del file Json</param>
        public async void GetSports(Action<Sports, Exception> callback, string uri)
        {
            Exception err = null;
            Sports results = null;
            try
            {
                var resultFileJson = await this.DownloadFileJson(uri);
                if (!string.IsNullOrEmpty(resultFileJson))
                {
                    // PARSER
                    results = JsonConvert.DeserializeObject<Sports>(resultFileJson);
                }
            }
            catch (Exception e)
            {
                err = e;
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \n" + e.ToString());
            }

            callback(results, err);
        }
    }
}
