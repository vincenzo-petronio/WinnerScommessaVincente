using GalaSoft.MvvmLight;
using System.Linq;
using System.Collections.Generic;
using WinnerSV.DataModel;
using WinnerSV.DataSample;
using GalaSoft.MvvmLight.Command;
using WinnerSV.Helpers;
using GalaSoft.MvvmLight.Messaging;
using WinnerSV.Common;
using System.Collections.ObjectModel;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// ViewModel associato alla vista Sports, contenente la lista di tutti i possibili
    /// Sport sui quali e' possibile effettuare delle scommesse.
    /// </summary>
    public class SportsViewModel : ViewModelBase
    {
        private IServiceAgent serviceAgent;
        private Sports sports;
        private ObservableCollection<Calcio> listCalcio;
        private ObservableCollection<Basket> listBasket;
        private ObservableCollection<Tennis> listTennis;
        private Incontro itemSelected;
        private Incontro itemSelectedStored;
        private bool isProgressIndicatorVisible = true;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public SportsViewModel(IServiceAgent srvAgn)
        {
            if (IsInDesignMode)
            {
                // TODO Fare injection con IDataService
                SportsData sd = new SportsData();
                listCalcio = new ObservableCollection<Calcio>(sd.ListCalcio);
                listBasket = new ObservableCollection<Basket>(sd.ListBasket);
                listTennis = new ObservableCollection<Tennis>(sd.ListTennis);
            }
            else
            {
                // RELAY COMMAND
                NavToPageCommand = new RelayCommand(IncontroSelectedCommand);

                // SERVICE AGENT
                this.serviceAgent = srvAgn;
                this.serviceAgent.GetSports((sports, err) =>
                    {
                        if (err != null)
                        {
                            System.Diagnostics.Debug.WriteLine("[SportsViewModel] \r" + err.Message);
                        }
                        else
                        {
                            // CALCIO
                            this.ListCalcio = new ObservableCollection<Calcio>(sports.Calcio);
                            RaisePropertyChanged(() => GroupedCalcio);

                            // TENNIS
                            this.listTennis = new ObservableCollection<Tennis>(sports.Tennis);
                            RaisePropertyChanged(() => GroupedTennis);

                            // BASKET
                            this.listBasket = new ObservableCollection<Basket>(sports.Basket);
                            RaisePropertyChanged(() => GroupedBasket);
                        }
                        // Nascondo la progress
                        IsProgressIndicatorVisible = false;
                    }
                , Constants.URL_JSON);
            }
        }

        /// <summary>
        /// Proprieta' in binding con la Visibility del ProgressIndicator.
        /// </summary>
        public bool IsProgressIndicatorVisible
        {
            get
            {
                return isProgressIndicatorVisible;
            }

            set
            {
                if(isProgressIndicatorVisible != value)
                {
                    isProgressIndicatorVisible = value;
                    RaisePropertyChanged(() => IsProgressIndicatorVisible);
                }
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
        /// Conserva l'oggetto selezionato per essere utilizzato da altri ViewModel.
        /// </summary>
        public Incontro ItemSelectedStored
        {
            get
            {
                return itemSelectedStored;
            }

            set
            {
                if (itemSelectedStored != value)
                {
                    itemSelectedStored = value;
                    RaisePropertyChanged(() => ItemSelectedStored);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Calcio.
        /// </summary>
        public ObservableCollection<Calcio> ListCalcio
        {
            get 
            { 
                return listCalcio; 
            }

            set
            {
                if (listCalcio != value)
                {
                    listCalcio = value;
                    RaisePropertyChanged(() => ListCalcio);
                }
            }
        }
        
        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Basket.
        /// </summary>
        public ObservableCollection<Basket> ListBasket
        {
            get 
            { 
                return listBasket; 
            }

            set
            {
                if (listBasket != value)
                {
                    listBasket = value;
                    RaisePropertyChanged(() => ListBasket);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Tennis.
        /// </summary>
        public ObservableCollection<Tennis> ListTennis
        {
            get 
            { 
                return listTennis; 
            }

            set
            {
                if (listTennis != value)
                {
                    listTennis = value;
                    RaisePropertyChanged(() => ListTennis);
                }
            }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la LongListSelector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedCalcio
        {
            get
            {
                if (listCalcio != null)
                {
                    // linq che per ogni campionato prende la lista di incontri associati al compionato, 
                    // e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                    var listGruppi =
                        from calcio in listCalcio
                        orderby calcio.NomeCampionato

                        from incontro in calcio.ElencoIncontriCalcio
                        group incontro by calcio.NomeCampionato into listaCalcioGroup
                        select new KeyedList<string, Incontro>(listaCalcioGroup);

                    return new List<KeyedList<string, Incontro>>(listGruppi);
                }
                else
                {
                    return new List<KeyedList<string, Incontro>>();
                }
            }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la Long List Selector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedTennis
        {
            get
            {
                if (listTennis != null)
                {
                    // linq che per ogni campionato prende la lista di incontri associati al compionato, 
                    // e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                    var listGruppi =
                        from tennis in listTennis
                        orderby tennis.NomeCampionato

                        from incontro in tennis.ElencoIncontriTennis
                        group incontro by tennis.NomeCampionato into listaTennisGroup
                        select new KeyedList<string, Incontro>(listaTennisGroup);

                    return new List<KeyedList<string, Incontro>>(listGruppi);
                }
                else
                {
                    return new List<KeyedList<string, Incontro>>();
                }
            }
        }

        /// <summary>
        /// Crea una lista di incontri ragruppati associati ai campionati per la Long List Selector
        /// </summary>
        public List<KeyedList<string, Incontro>> GroupedBasket
        {
            get
            {
                if (listBasket != null)
                {
                    // linq che per ogni campionato prende la lista di incontri associati al compionato, 
                    // e per ogni incontro crea gli oggetti KeyedList che mi serve per creare i gruppi 
                    var listGruppi =
                        from basket in listBasket
                        orderby basket.NomeCampionato

                        from incontro in basket.ElencoIncontriBasket
                        group incontro by basket.NomeCampionato into listaBasketGroup
                        select new KeyedList<string, Incontro>(listaBasketGroup);

                    return new List<KeyedList<string, Incontro>>(listGruppi);
                }
                else
                {
                    return new List<KeyedList<string, Incontro>>();
                }
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
                ItemSelectedStored = ItemSelected;
                Messenger.Default.Send<NavToPage>(new NavToPage { PageName = "IncontroView" });
                ItemSelected = null;
            }
        }

    }
}
