using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinnerSV.DataModel
{
    public class ServiceAgentSports : IServiceAgent
    {
        private string lingua = "it";
        private string url = "http://winnerscommessavincente.altervista.org/Json/{0}_scommesse.json";
        private List<Calcio> listCalcio = new List<Calcio>();
        private List<Tennis> listTennis = new List<Tennis>();
        private List<Basket> listBasket = new List<Basket>();

        /// <summary>
        /// la lingua deve essere es: "it", "en"
        /// serve per construire l'url "http://winnerscommessavincente.altervista.org/Json/{0}_scommesse.json"
        /// </summary>
        /// <param name="lingua"></param>
        public ServiceAgentSports(string lingua)
        {
            this.lingua = lingua;
            this.url = String.Format(url, this.lingua);
            parseFileJson();
        }

        public List<Calcio> GetListCalcio()
        {
            throw new NotImplementedException();
        }

        public List<Basket> GetListBasket()
        {
            throw new NotImplementedException();
        }

        public List<Tennis> GetListTennis()
        {
            throw new NotImplementedException();
        }

        private void parseFileJson()
        {
            System.Diagnostics.Debug.WriteLine("[DataSports] " + "download content = " + this.url);

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += HttpCompleted;
            wc.DownloadStringAsync(new Uri(url));

        }

        private void HttpCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var resultString = e.Result;
                Sports deserializedSports = JsonConvert.DeserializeObject<Sports>(resultString);

            }
        }
    }
}
