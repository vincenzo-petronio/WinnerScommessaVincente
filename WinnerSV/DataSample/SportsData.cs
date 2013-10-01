using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnerSV.DataModel;

namespace WinnerSV.DataSample
{
    public class SportsData : ViewModelBase
    {
        private ObservableCollection<Calcio> listCalcio;
        private ObservableCollection<Basket> listBasket;
        private ObservableCollection<Tennis> listTennis;

        #region lista campionati
        private List<string> nomeCampionati = new List<string>()
        {
            "Coppa di Danimarca",
            "2a Divisione svizzera",
            "Campionato israeliano",
            "Spagna Primera Divisi&oacute;n",
            "2a Divisione austriaca",
            "Francia Ligue 1",
            "Finlandia Veikkausliiga",
            "Germania Bundesliga"
        };
        #endregion

        #region lista team casa
        private List<string> listTeamCasa = new List<string>()
        {
            "Jong PSV Eindhoven",
            "Jong Ajax",
            "Hapoel Beer Sheva",
            "Union Berlino",
            "Arminia Bielefeld",
            "Jong PSV Eindhoven",
            "Fsv Francoforte",
            "Norwich"
        };
        #endregion

        #region lista team fuori casa
        private List<string> listTeamFCasa = new List<string>()
        {
            "Swansea",
            "Manchester ",
            "Repubblica Ceca vincente coppa in mano",
            "Russia Vincente coppa in mano",
            "Oostende (qualif.)",
            "Fc Bayern Monaco",
            "Kerber A.",
            "Wawrinka S."
        };
        #endregion

        #region lista date
        List<string> date = new List<string>()
        {
            "2013-10-05T13:00:00",
            "2013-10-06T13:30:00",
            "2013-10-06T13:30:00",
            "2013-10-06T13:30:00",
            "2013-10-05T13:00:00",
            "2013-10-06T13:30:00",
            "2013-09-30T20:00:00",
            "2013-10-06T15:00:00",
            "2013-10-05T16:00:00",
            "2013-10-05T16:00:00",
            "2013-09-30T21:00:00",
            "2013-09-30T21:00:00"
        };
        #endregion

        #region lista IdMatch
        List<string> listaIdMatch = new List<string>()
        {
            "1179219",
            "1167666",
            "1179215",
            "1173567",
            "1174333",
            "1174332",
            "1174337"
        };
        #endregion

        #region lista Q1 Q2 QX HC1 HCX HC2 Under Over DC1X DCX2 DC12
        List<string> listQuote = new List<string>()
        {
            "2.15",
            "1.90",
            "1.70",
            "2.00",
            "2.05",
            "2.30",
            "3.60",
            "5.30",
            "5.80",
            "9.75",
            "10.00",
            "9.00"
        };
        #endregion

        #region list Handicap
        List<string> listHC = new List<string>()
        {
            "1:0",
            "0:1"
        };
        #endregion

        public SportsData()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                listCalcio = PopulateListCalcio(10);
                listBasket = PopulateListBasket(3);
                listTennis = PopulateListTennis(5);
            }

        }

        public ObservableCollection<Calcio> ListCalcio
        {
            get { return listCalcio; }
        }

        public ObservableCollection<Basket> ListBasket
        {
            get { return listBasket; }
        }

        public ObservableCollection<Tennis> ListTennis
        {
            get { return listTennis; }
        }

        private ObservableCollection<Calcio> PopulateListCalcio(int numCampionati)
        {
            ObservableCollection<Calcio> listCalcio = new ObservableCollection<Calcio>();
            Random r = new Random();
            for (int i = 0; i < numCampionati; ++i)
            {
                Calcio calcio = new Calcio();

                int index = r.Next(nomeCampionati.Count);
                calcio.NomeCampionato = nomeCampionati[index];

                List<Incontro> elencoIncontriCalcio = new List<Incontro>();
                int numPartiteCampionato = r.Next(1,10);
                for (int j = 0; j < numPartiteCampionato; ++j)
                {
                    Incontro elencoIncontri = new Incontro();
                    
                    index = r.Next(listTeamCasa.Count);
                    elencoIncontri.TeamCasa = listTeamCasa[index];

                    index = r.Next(listTeamFCasa.Count);
                    elencoIncontri.TeamFCasa = listTeamFCasa[index];

                    index = r.Next(date.Count);
                    elencoIncontri.Data = date[index];

                    index = r.Next(listaIdMatch.Count);
                    elencoIncontri.IdMatch = listaIdMatch[index];

                    index = r.Next(listHC.Count);
                    elencoIncontri.HC = listHC[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q1 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q2 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.QX = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.HC1 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.HC2 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.HCX = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.TotalScore = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.OVER = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.UNDER = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.DC12 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.DC1X = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.DCX2 = listQuote[index];

                    elencoIncontriCalcio.Add(elencoIncontri);
                }

                calcio.ElencoIncontriCalcio = elencoIncontriCalcio;
                listCalcio.Add(calcio);
            }
            return listCalcio;
        }

        /// <summary>
        /// aggiunge solo 2 scommesse per partita "Under/Over" e "1 x 2"
        /// </summary>
        /// <param name="numCampionati"></param>
        /// <returns></returns>
        private ObservableCollection<Basket> PopulateListBasket(int numCampionati)
        {
            ObservableCollection<Basket> baskets = new ObservableCollection<Basket>();
            Random r = new Random();
            for (int i = 0; i < numCampionati; ++i)
            {
                Basket basket = new Basket();

                int index = r.Next(nomeCampionati.Count);
                basket.NomeCampionato = nomeCampionati[index];

                List<Incontro> elencoIncontriBasket = new List<Incontro>();
                int numPartiteCampionato = r.Next(1, 10);
                for (int j = 0; j < numPartiteCampionato; ++j)
                {
                    Incontro elencoIncontri = new Incontro();

                    index = r.Next(listTeamCasa.Count);
                    elencoIncontri.TeamCasa = listTeamCasa[index];

                    index = r.Next(listTeamFCasa.Count);
                    elencoIncontri.TeamFCasa = listTeamFCasa[index];

                    index = r.Next(date.Count);
                    elencoIncontri.Data = date[index];

                    index = r.Next(listaIdMatch.Count);
                    elencoIncontri.IdMatch = listaIdMatch[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q1 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q2 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.QX = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.TotalScore = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.OVER = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.UNDER = listQuote[index];

                    elencoIncontriBasket.Add(elencoIncontri);

                }

                basket.ElencoIncontriBasket = elencoIncontriBasket;
                baskets.Add(basket);
            }
            return baskets;
        }

        /// <summary>
        /// aggiunge solo una scommessa per partita e "1 x 2"
        /// </summary>
        /// <param name="numCampionati"></param>
        /// <returns></returns>
        private ObservableCollection<Tennis> PopulateListTennis(int numCampionati)
        {
            ObservableCollection<Tennis> tenniss = new ObservableCollection<Tennis>();
            Random r = new Random();
            for (int i = 0; i < numCampionati; ++i)
            {
                Tennis tennis = new Tennis();

                int index = r.Next(nomeCampionati.Count);
                tennis.NomeCampionato = nomeCampionati[index];

                List<Incontro> elencoIncontriTennis = new List<Incontro>();
                int numPartiteCampionato = r.Next(1, 10);
                for (int j = 0; j < numPartiteCampionato; ++j)
                {
                    Incontro elencoIncontri = new Incontro();

                    index = r.Next(listTeamCasa.Count);
                    elencoIncontri.TeamCasa = listTeamCasa[index];

                    index = r.Next(listTeamFCasa.Count);
                    elencoIncontri.TeamFCasa = listTeamFCasa[index];

                    index = r.Next(date.Count);
                    elencoIncontri.Data = date[index];

                    index = r.Next(listaIdMatch.Count);
                    elencoIncontri.IdMatch = listaIdMatch[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q1 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.Q2 = listQuote[index];

                    index = r.Next(listQuote.Count);
                    elencoIncontri.QX = listQuote[index];

                    elencoIncontriTennis.Add(elencoIncontri);

                }

                tennis.ElencoIncontriTennis = elencoIncontriTennis;
                tenniss.Add(tennis);
            }
            return tenniss;
        }
    }
}
