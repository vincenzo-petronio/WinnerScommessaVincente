using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnerSV.Common;
using WinnerSV.DataModel;
using WinnerSV.Helpers;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// ViewModel associato al controllo che include l'anteprima della schedina.
    /// </summary>
    public class AnteprimaSchedinaViewModel : ViewModelBase
    {
        private IDataAccessDb dataAccessDb;
        private Schedina selectedSchedina;
        private string titleSchedina;

        public AnteprimaSchedinaViewModel(IDataAccessDb db)
        {
            if (IsInDesignMode)
            {
                // design-mode
            }
            else
            {
                this.dataAccessDb = db;

                // RELAY COMMAND
                NavToPageCommand = new RelayCommand(async () =>
                {
                    if (this.TitleSchedina != Constants.TITLE_SCHEDINA_DEFAULT &&
                        !string.IsNullOrEmpty(this.TitleSchedina))
                    {
                        System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] " + "Tapped NavToPageCommand: " 
                            + TitleSchedina.ToString());

                        bool isCompleted = await dataAccessDb.SetSchedina(this.TitleSchedina);
                        if (isCompleted)
                        {
                            Messenger.Default.Send<NavToPage>(new NavToPage { PageName = "SportsView" });
                        }
                        else
                        {
                            // TODO Notificare l'errore!
                        }
                    }
                    else
                    {
                        // TODO 
                        // Notificare il titolo di default da sovrascrivere!
                    }
                });

                // Inizializzo il titolo con un nome di default
                this.titleSchedina = Constants.TITLE_SCHEDINA_DEFAULT;

                SetSelectedSchedina();
            }
        }

        public Schedina SelectedSchedina
        {
            get
            {
                return selectedSchedina;
            }

            set
            {
                if(selectedSchedina != value)
                {
                    selectedSchedina = value;
                    RaisePropertyChanged(() => SelectedSchedina);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la casella di testo nello XAML che 
        /// contiene il titolo della schedina, editabile dall'utente.
        /// </summary>
        public string TitleSchedina
        {
            get
            {
                return titleSchedina;
            }

            set
            {
                if (titleSchedina != value)
                {
                    titleSchedina = value;
                    RaisePropertyChanged(() => TitleSchedina);
                }
            }
        }
       
        private async void SetSelectedSchedina()
        {
            Schedina s = await dataAccessDb.GetSchedina("Nome Cognome 110");
            SelectedSchedina = s;
        }

        /// <summary>
        /// RelayCommand per la Navigation.
        /// </summary>
        public RelayCommand NavToPageCommand
        {
            get;
            private set;
        }

    }
}
