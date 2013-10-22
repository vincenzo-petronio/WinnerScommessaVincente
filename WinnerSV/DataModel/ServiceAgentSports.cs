//-----------------------------------------------------------------------
// <copyright file="ServiceAgentSports.cs">
//    Copyright (c) 2013 Vincenzo Petronio <vincenzo.petronio"AT"gmail.com>. All rights reserved.
// </copyright>
// <license>
//    This file is part of Winner - Scommessa vincente.
//
//    Winner - Scommessa vincente is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 2 of the License, or
//    (at your option) any later version.
//
//    Winner - Scommessa vincente is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Winner - Scommessa vincente.  If not, see <http://www.gnu.org/licenses/>.
// </license>
//-----------------------------------------------------------------------
      
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
        /// <summary>
        /// Costruttore.
        /// </summary>
        public ServiceAgentSports()
        {
        }

        /// <summary>
        /// Ricava dai settings la lingua di sistema.
        /// </summary>
        private string RilevaLingua()
        {
            string lingua = "it";

            try
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(Constants.SETTINGS_KEY_CORRENTE_LINGUA))
                {
                    // La lingua memorizzata e' nel formato BCP-47.
                    // Trasformo en-US nel formato en.
                    lingua = ((string)settings[Constants.SETTINGS_KEY_CORRENTE_LINGUA]).Split('-')[0];
                }
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \t" + "currentLanguage = " + lingua);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \n" + e.ToString());
            }
            finally
            { 
            }

            return lingua;
        }

        /// <summary>
        /// Consente di scaricare un file da URL remoto.
        /// </summary>
        /// <param name="uri">string Path del file remoto</param>
        /// <returns>string Contenuto del file remoto ricevuto dalla risposta Http</returns>
        private async Task<string> DownloadRemoteFile(string uri)
        {
            var downloaded = string.Empty;
            
            if (!string.IsNullOrEmpty(uri))
            {
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - URI] \t" + uri);
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    HttpWebRequest client = (HttpWebRequest)WebRequest.Create(uri);
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
        /// Restituisce una oggetto Sports con i dati ottenuti dal JSON.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="uri">string Path finale del file Json</param>
        public async void GetSports(Action<Sports, Exception> callback, string uriFileName)
        {
            Exception err = null;
            Sports results = null;

            string uri = Constants.URL_SUBDOMAIN_JSON + RilevaLingua() + uriFileName;
            try
            {
                var resultFileJson = await this.DownloadRemoteFile(uri);
                if (!string.IsNullOrEmpty(resultFileJson))
                {
                    // PARSER
                    Stopwatch sw = Stopwatch.StartNew();
                    results = JsonConvert.DeserializeObject<Sports>(resultFileJson);
                    sw.Stop();
                    System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - GetSports] \t" 
                        + "Parser completato in {0} s.", sw.Elapsed.TotalSeconds);
                }
            }
            catch (Exception e)
            {
                err = e;
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - GetSports] \n" + e.ToString());
            }

            callback(results, err);
        }
    }
}
