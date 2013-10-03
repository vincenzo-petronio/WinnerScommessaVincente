using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnerSV.DataModel;
using WinnerSV.DataSample;
using WinnerSV.Helpers;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// ViewModel associato alla vista Panorama.
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<Schedina> schedine;
        private IDataAccessDb dataAccessDb;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public HomeViewModel(IDataAccessDb db)
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                // TODO Fare injection con IDataService
                PanoramaData pd = new PanoramaData();
                Schedine = pd.Schedine;
            }
            else
            {
                // Code runs "for real"

                // DB
                this.dataAccessDb = db;

                // RELAY COMMAND
                NavToPageCommand = new RelayCommand<string>((args) =>
                {
                    System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "Tapped NavToPageCommand: " + args.ToString());
                    string pageName = string.Empty;

                    switch (args.ToString())
                    {
                        case "PanoramaPivot1": pageName = "SportsView"; break;
                        case "PanoramaPivot2": pageName = "AnteprimaSchedina"; break;
                        default: break;
                    }

                    Messenger.Default.Send<NavToPage>(new NavToPage { PageName = pageName });
                });
                
                PopolaListaSchedine();
            }
        }

        /// <summary>
        /// Consente di popolare il Pivot delle Schedine con i dati presenti nel DB.
        /// </summary>
        private async void PopolaListaSchedine()
        {
            // TEST
            DbData d = new DbData();
            await d.PopolaDb();

            this.Schedine = new ObservableCollection<Schedina>(await dataAccessDb.GetSchedine());
        }

        /// <summary>
        /// RelayCommand per la Navigation.
        /// </summary>
        public RelayCommand<string> NavToPageCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot 2.
        /// </summary>
        public ObservableCollection<Schedina> Schedine
        {
            get 
            { 
                return schedine; 
            }

            set
            {
                if (schedine != value)
                {
                    schedine = value;
                    RaisePropertyChanged(() => Schedine);
                }
            }
        }
    }
}
