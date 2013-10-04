using GalaSoft.MvvmLight;
using System.Linq;
using System.Collections.Generic;
using WinnerSV.DataModel;
using WinnerSV.DataSample;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WinnerSV.Helpers;
using GalaSoft.MvvmLight.Messaging;

namespace WinnerSV.ViewModels
{

    public class SportsViewModel : ViewModelBase
    {
        private IServiceAgent serviceAgent;
        private List<Calcio> listCalcio;
        private List<Basket> listBasket;
        private List<Tennis> listTennis;

        private Incontro itemSelected;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public SportsViewModel(IServiceAgent srvAgn)
        {
            if (IsInDesignMode)
            {
                // TODO Fare injection con IDataService
                SportsData sd = new SportsData();
                listCalcio = sd.ListCalcio;
                listBasket = sd.ListBasket;
                listTennis = sd.ListTennis;
            }
            else
            {
                // real code
                NavToPageCommand = new RelayCommand(IncontroSelectedCommand);
                
                SportsData sd = new SportsData();
                listCalcio = sd.ListCalcio;
                listBasket = sd.ListBasket;
                listTennis = sd.ListTennis;
            }
        }

        /// <summary>
        /// Binding con la proprietà SelectedItem del LongListSelector
        /// </summary>
        public Incontro ItemSelected
        {
            get
            {
                return itemSelected;
            }

            set
            {
                if (itemSelected != value)
                {
                    itemSelected = value;
                    RaisePropertyChanged(() => ItemSelected);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Calcio.
        /// </summary>
        public List<Calcio> ListCalcio
        {
            get { return listCalcio; }
        }
        
        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Basket.
        /// </summary>
        public List<Basket> ListBasket
        {
            get { return listBasket; }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Tennis.
        /// </summary>
        public List<Tennis> ListTennis
        {
            get { return listTennis; }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la Long List Selector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedCalcio
        {
            get
            {
                //linq che per ogni campionato prende la lista di incontri associati al compionato, 
                //e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                var listGruppi =
                    from calcio in listCalcio
                    orderby calcio.NomeCampionato

                    from incontro in calcio.ElencoIncontriCalcio
                    group incontro by calcio.NomeCampionato into listaCalcioGroup
                    select new KeyedList<string, Incontro>(listaCalcioGroup);

                return new List<KeyedList<string, Incontro>>(listGruppi);
            }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la Long List Selector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedTennis
        {
            get
            {
                //linq che per ogni campionato prende la lista di incontri associati al compionato, 
                //e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                var listGruppi =
                    from tennis in listTennis
                    orderby tennis.NomeCampionato

                    from incontro in tennis.ElencoIncontriTennis
                    group incontro by tennis.NomeCampionato into listaTennisGroup
                    select new KeyedList<string, Incontro>(listaTennisGroup);

                return new List<KeyedList<string, Incontro>>(listGruppi);
            }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la Long List Selector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedBasket
        {
            get
            {
                //linq che per ogni campionato prende la lista di incontri associati al compionato, 
                //e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                var listGruppi =
                    from basket in listBasket
                    orderby basket.NomeCampionato

                    from incontro in basket.ElencoIncontriBasket
                    group incontro by basket.NomeCampionato into listaBasketGroup
                    select new KeyedList<string, Incontro>(listaBasketGroup);

                return new List<KeyedList<string, Incontro>>(listGruppi);
            }
        }

        /// <summary>
        /// RelayCommand per la Navigation a dettaglio incontro
        /// </summary>
        public RelayCommand NavToPageCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Permette la navigazione alla vista successiva
        /// </summary>
        private void IncontroSelectedCommand()
        {
            if (itemSelected != null)
            {
                System.Diagnostics.Debug.WriteLine("[SPORTSVIEWMODEL] " + "Tapped NavToPageCommand! " + itemSelected.TeamCasa + " vs " + itemSelected.TeamFCasa);
                Messenger.Default.Send<NavToPage>(new NavToPage { PageName = "IncontroView" });
                ItemSelected = null;
            }
        }

    }
}
