//-----------------------------------------------------------------------
// <copyright file="AnteprimaSchedinaViewModel.cs">
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
      
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using WinnerSV.DataModel;
using WinnerSV.Helpers;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// ViewModel unico associato alle diverse viste e controlli contenenti 
    /// l'anteprima della Schedina giocata.
    /// </summary>
    public class AnteprimaSchedinaViewModel : ViewModelBase
    {
        private IDataAccessDb dataAccessDb;
        private Schedina selectedSchedina;
        private Incontro selectedIncontro;
        private Scommessa selectedScommessa;
        ////private Scommessa scommessaStored;
        private ObservableCollection<Scommessa> listScommesse;
        private bool isProgressIndicatorVisible = false;

        /// <summary>
        /// Costruttore.
        /// </summary>
        /// <param name="db">IDataAccessDb</param>
        public AnteprimaSchedinaViewModel(IDataAccessDb db)
        {
            if (IsInDesignMode)
            {
                // design-mode
            }
            else
            {
                // INIT
                this.selectedSchedina = new Schedina();
                this.selectedScommessa = new Scommessa();
                this.selectedIncontro = new Incontro();
                this.listScommesse = new ObservableCollection<Scommessa>();

                // DB
                this.dataAccessDb = db;

                // RELAY COMMAND
                NavToPageCommand = new RelayCommand(InsertSchedinaCommandExecute);
                UpdateScommessaCommand = new RelayCommand<string>(UpdateScommessaCommandExecute);
                DeleteScommessaCommand = new RelayCommand<Scommessa>(DeleteScommessaCommandExecute);
                RemoveSelectedItemCommand = new RelayCommand(RemoveSelectedItemCommandExecute);
                AlreadyUpdateScommessaCommand = new RelayCommand(AlreadyUpdateScommessaCommandExecute);
            }
        }

        /// <summary>
        /// Esegue l'inizializzazione nel DB della nuova schedina, effettuando un controllo
        /// sulla validita' dei dati.
        /// </summary>
        private async void InsertSchedinaCommandExecute()
        {
            if (!string.IsNullOrEmpty(this.SelectedSchedina.Title))
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "Tapped NavToPageCommand: "
                    + SelectedSchedina.Title);

                bool isCompleted = await dataAccessDb.SetSchedina(SelectedSchedina.Title);
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
        }

        /// <summary>
        /// Inserisce o aggiorna nel DB una scommessa per una determinata schedina.
        /// </summary>
        /// <param name="obj"></param>
        private async void UpdateScommessaCommandExecute(string obj)
        {
            if (SelectedIncontro != null)
            {
                // Creo un nuovo Incontro con i dati delle quote da giocare.
                Incontro i = new Incontro();
                // Valori delle PRIMARY KEY da inviare sempre.
                i.TeamCasa = SelectedIncontro.TeamCasa;
                i.TeamFCasa = SelectedIncontro.TeamFCasa;
                i.Data = SelectedIncontro.Data;
                i.IdMatch = SelectedIncontro.IdMatch;

                // Quota giocata.
                switch (obj)
                {
                    case "OVER": { i.OVER = SelectedIncontro.OVER; i.TotalScore = SelectedIncontro.TotalScore; } break;
                    case "UNDER": { i.UNDER = SelectedIncontro.UNDER; i.TotalScore = SelectedIncontro.TotalScore; } break;
                    case "Q1":
                        {
                            i.Q1 = SelectedIncontro.Q1;
                            i.QX = string.Empty;
                            i.Q2 = string.Empty;
                        }
                        break;
                    case "QX":
                        {
                            i.QX = SelectedIncontro.QX;
                            i.Q1 = string.Empty;
                            i.Q2 = string.Empty;
                        }
                        break;
                    case "Q2":
                        {
                            i.Q2 = SelectedIncontro.Q2;
                            i.Q1 = string.Empty;
                            i.QX = string.Empty;
                        }
                        break;
                    ////case "HC": i.HC = SelectedIncontro.HC; break;
                    case "HC1": { i.HC1 = SelectedIncontro.HC1; i.HC = SelectedIncontro.HC; } break;
                    case "HC2": { i.HC2 = SelectedIncontro.HC2; i.HC = SelectedIncontro.HC; } break;
                    case "HCX": { i.HCX = SelectedIncontro.HCX; i.HC = SelectedIncontro.HC; } break;
                    case "DC1X": i.DC1X = SelectedIncontro.DC1X; break;
                    case "DC12": i.DC12 = SelectedIncontro.DC12; break;
                    case "DCX2": i.DCX2 = SelectedIncontro.DCX2; break;
                    case "Home12": i.Home12 = SelectedIncontro.Home12; break;
                    case "Away12": i.Away12 = SelectedIncontro.Away12; break;
                    default: break;
                }

                // Recupero la Schedina, poi passo l'oggetto Schedina e l'oggetto Incontro.
                // Dall'unione dei due ottengo un oggetto Scommessa che posso inserire nella tabella 
                // Scommessa
                var s = await dataAccessDb.GetSchedina(this.SelectedSchedina.Title);
                bool isCompleted = await dataAccessDb.UpdateScommessa(i, s);
                if (isCompleted)
                {
                    // Aggiorno la lista delle scommesse effettuate.
                    PopolaAnteprimaProperties();
                    // Aggiorno la proprietà in binding 
                    ////ScommessaStored = await dataAccessDb.GetScommessa(i, s);
                }
                else
                {
                    // TODO
                }
            }
        }

        /// <summary>
        /// Cancella dal DB una Scommessa di una determinata Schedina.
        /// </summary>
        private async void DeleteScommessaCommandExecute(Scommessa s)
        {
            if (s != null && s.GetType() == typeof(Scommessa))
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "Tapped DeleteScommmessaCommand: " + s.IdScommessa.ToString() + "," + s.IdMatch.ToString());
                bool isDeleted = await dataAccessDb.DeleteScommessa(s);
                if (isDeleted)
                {
                    ListScommesse.Remove(s);
                }
                else
                {
                    // TODO 
                    // Notificare l'errore nella UI.
                }
            }
        }

        /// <summary>
        /// Riporta a null le proprieta' in binding.
        /// </summary>
        private void RemoveSelectedItemCommandExecute()
        {
            // WORK-AROUND
            // Con il BackKey da IncontroView a SportsView, disabilito l'item selezionato.
            // Necessario perche' con il LongListSelector l'evento nel Command è SelectionChanged,
            // non Tap (il Tap intercetta anche l'Header).
            ////this.SelectedIncontro = null;
            ////this.ScommessaStored = null;
        }

        /// <summary>
        /// Restituisce una immagine della Scommessa presente nel DB.
        /// </summary>
        private async void AlreadyUpdateScommessaCommandExecute()
        {
            System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "AlreadyUpdateScommessaCommandExecute routine");

            if (SelectedIncontro != null)
            {
                // Creo un nuovo Incontro con i dati delle quote da giocare.
                Incontro i = new Incontro();
                // Valori delle PRIMARY KEY da inviare sempre.
                i.TeamCasa = SelectedIncontro.TeamCasa;
                i.TeamFCasa = SelectedIncontro.TeamFCasa;
                i.Data = SelectedIncontro.Data;
                i.IdMatch = SelectedIncontro.IdMatch;

                // Recupero la Scommessa, nota la Schedina e l'Incontro selezionati.
                ////Schedina s = await dataAccessDb.GetSchedina(this.SelectedSchedina.Title);
                ////ScommessaStored = await dataAccessDb.GetScommessa(i, s);
            }
        }

        /// <summary>
        /// Boolean per impostare a True/False la Visibility di un elemento nello XAML. 
        /// </summary>
        public bool IsProgressIndicatorVisible
        {
            get
            {
                return isProgressIndicatorVisible;
            }

            set
            {
                if (isProgressIndicatorVisible != value)
                {
                    isProgressIndicatorVisible = value;
                    RaisePropertyChanged(() => IsProgressIndicatorVisible);
                }
            }
        }

        /// <summary>
        /// Rappresenta la scommessa gia' memorizzata nel DB, 
        /// per una determinata Schedina e un Incontro.
        /// </summary>
        ////public Scommessa ScommessaStored
        ////{
        ////    get
        ////    {
        ////        System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "GET ScommessaStored");
        ////        return scommessaStored;
        ////    }

        ////    set
        ////    {
        ////        if(scommessaStored != value)
        ////        {
        ////            scommessaStored = value;
        ////            System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "SET ScommessaStored");
        ////            RaisePropertyChanged(() => ScommessaStored);
        ////        }
        ////    }
        ////}
        

        /// <summary>
        /// Rappresenta la Schedina giocata, sia nel caso di una nuova schedina, che 
        /// in quello di una precedente schedina gia' giocata ma modificabile.
        /// </summary>
        public Schedina SelectedSchedina
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "GET SelectedSchedina");
                return selectedSchedina;
            }

            set
            {
                if (selectedSchedina != value && value != null)
                {
                    selectedSchedina = value;
                    System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "SET SelectedSchedina");
                    RaisePropertyChanged(() => SelectedSchedina);
                }
            }
        }

        /// <summary>
        /// Rappresenta l'incontro sportivo selezionato all'interno di una Schedina, e 
        /// dal quale posso scegliere la scommessa da effettuare.
        /// </summary>
        public Incontro SelectedIncontro
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "GET SelectedIncontro");
                return selectedIncontro;
            }

            set
            {
                if (selectedIncontro != value && value != null)
                {
                    selectedIncontro = value;
                    System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "SET SelectedIncontro");
                    RaisePropertyChanged(() => SelectedIncontro);
                }
            }
        }

        /// <summary>
        /// Rappresenta la scommessa effettuata, per un determinato incontro e all'interno
        /// di una Schedina. E' l'immagine del contenuto nel DB.
        /// </summary>
        public Scommessa SelectedScommessa
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "GET SelectedScommessa");
                return selectedScommessa;
            }

            set
            {
                if (selectedScommessa != value)
                {
                    selectedScommessa = value;
                    System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "SET SelectedScommessa");
                    RaisePropertyChanged(() => SelectedScommessa);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista nella vista Panorama, all'interno del Pivot Schedine.
        /// </summary>
        public ObservableCollection<Scommessa> ListScommesse
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "GET ListScommesse");
                return listScommesse;
            }

            set
            {
                if (listScommesse != value)
                {
                    listScommesse = value;
                    System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "SET ListScommesse");
                    RaisePropertyChanged(() => ListScommesse);
                }
            }
        }

        /// <summary>
        /// Task per popolare le Properties in Binding con lo XAML.
        /// Il metodo viene chiamato dal code-behind delle varie View.
        /// </summary>
        /// <returns></returns>
        public async void PopolaAnteprimaProperties()
        {
            System.Diagnostics.Debug.WriteLine("[ANTEPRIMASCHEDINAVIEWMODEL] \t" + "*** REFRESH ANTEPRIMA ***");
            var listScommesseFromDb = await dataAccessDb.GetScommesse(SelectedSchedina.Title);
            foreach (var s in listScommesseFromDb)
            {
                if (ListScommesse.Contains(s))
                {
                    ListScommesse.Remove(s);
                }
                ListScommesse.Add(s);
            }
        }

        /// <summary>
        /// RelayCommand per la Navigation.
        /// </summary>
        public RelayCommand NavToPageCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per aggiornare nel DB la scommessa effettuata.
        /// </summary>
        public RelayCommand<string> UpdateScommessaCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per la cancellazione di un elemento Scommessa dalla lista.
        /// </summary>
        public RelayCommand<Scommessa> DeleteScommessaCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per disabilitare l'elemento selezionato nelle liste.
        /// </summary>
        public RelayCommand RemoveSelectedItemCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per controllare le Scommesse gia' effettuate.
        /// </summary>
        public RelayCommand AlreadyUpdateScommessaCommand
        {
            get;
            private set;
        }

    }
}
