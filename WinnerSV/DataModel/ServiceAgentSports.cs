using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Threading.Tasks;
using WinnerSV.Common;
using WinnerSV.Helpers;

namespace WinnerSV.DataModel
{
    public class ServiceAgentSports : IServiceAgent
    {
        private string currentLanguage = string.Empty;
        private string url = string.Empty;
        private List<Calcio> listCalcio = new List<Calcio>();
        private List<Tennis> listTennis = new List<Tennis>();
        private List<Basket> listBasket = new List<Basket>();

        public ServiceAgentSports()
        {
            //mi serve per construire l'url da dove scarico il json
            RilevaLingua();
            //si parsa il file json popolando le 3 liste dei sport diversi
            //ParseFileJson();
        }

        /// <summary>
        /// la lingua deve essere es: "it", "en"
        /// serve per construire l'url "http://winnerscommessavincente.altervista.org/Json/{0}_scommesse.json"
        /// </summary>
        private void RilevaLingua()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            string lingua = "it";
            if (settings.Contains(Constants.SETTINGS_KEY_CORRENTE_LINGUA))
            {
                lingua = ((string)settings[Constants.SETTINGS_KEY_CORRENTE_LINGUA]).Split('-')[0];
            }
            currentLanguage = lingua;
            System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \t" + "currentLanguage = " + currentLanguage);
        }

        /// <summary>
        /// fa partire il download del file json
        /// </summary>
        //private void DownloadFileJson()
        //{
        //    this.url = Constants.URL_SUBDOMAIN_JSON + this.currentLanguage + Constants.URL_JSON;
        //    System.Diagnostics.Debug.WriteLine("[ServiceAgentSports] \t" + "download content = " + this.url);

        //    WebClient wc = new WebClient();
        //    wc.DownloadStringCompleted += HttpCompleted;
        //    wc.DownloadStringAsync(new Uri(url));
        //}

        /// <summary>
        /// parsa il content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ////private void HttpCompleted(object sender, DownloadStringCompletedEventArgs e)
        ////{
        ////    if (e.Error == null)
        ////    {
        ////        var resultString = e.Result;
        ////        Sports deserializedSports = JsonConvert.DeserializeObject<Sports>(resultString);
        ////        if (deserializedSports != null)
        ////        {
        ////            this.listCalcio = deserializedSports.Calcio;
        ////            this.listTennis = deserializedSports.Tennis;
        ////            this.listBasket = deserializedSports.Basket;
        ////        }
        ////    }
        ////}

        private async Task<string> DownloadFileJson(string uri)
        {
            var downloaded = string.Empty;
            this.url = Constants.URL_SUBDOMAIN_JSON + this.currentLanguage + uri;

            try
            {
                HttpWebRequest client = (HttpWebRequest)WebRequest.Create(this.url);
                client.Method = "Get";
                HttpWebResponse response = (HttpWebResponse)await client.GetResponseAsync();

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("NotFound");
                }
                else
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        downloaded = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[ServiceAgentSports - DownloadFileJson] \n" + ex.ToString());
            }
            return downloaded;
        }


        public async void GetSports(Action<Sports, Exception> callback, string uri)
        {
            Exception err = null;
            Sports results = null;
            try
            {
                var resultFileJson = await this.DownloadFileJson(uri);
                if (!string.IsNullOrEmpty(resultFileJson))
                {
                    results = JsonConvert.DeserializeObject<Sports>(resultFileJson);
                }
            }
            catch (Exception e)
            {
                err = e;
            }

            callback(results, err);
        }
    }
}
