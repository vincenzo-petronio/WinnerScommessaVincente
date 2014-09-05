//-----------------------------------------------------------------------
// <copyright file="HomeViewModel.cs">
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
    /// ViewModel associato alla vista Panorama.
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<Schedina> schedine;
        private Schedina nuovaSchedina;
        private IDataAccessDb dataAccessDb;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public HomeViewModel(IDataAccessDb db)
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                
                // INIT
                this.nuovaSchedina = new Schedina() { Title = Resources.AppResources.PanoramaPivot1ItemTitle };
                this.schedine = new ObservableCollection<Schedina>();

                // DB
                this.dataAccessDb = db;

                // RELAY COMMAND
                NavToPageCommand = new RelayCommand<Schedina>((s) =>
                {
                    if (s != null)
                    {
                        System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "Tapped NavToPageCommand: " + s.Title.ToString());
                        string pageName = "SchedinaView";
                        string pageParameter = s.Title.ToString();

                        Messenger.Default.Send<NavToPage>(new NavToPage { PageName = pageName, PageParameter = pageParameter });
                    }
                });

                DeleteSchedinaCommand = new RelayCommand<Schedina>(async (s) => 
                {
                    if (s != null && s.GetType() == typeof(Schedina))
                    {
                        System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "Tapped DeleteCommand: " + s.Title.ToString());
                        bool isDeleted = await dataAccessDb.DeleteSchedina(s.Id);
                        if (isDeleted)
                        {
                            Schedine.Remove(s);
                        }
                        else
                        {
                            // TODO 
                            // Notificare l'errore nella UI.
                        }
                    }
                });

                RefreshBindingData = new RelayCommand(PopolaPanoramaProperties);
            }
        }

        /// <summary>
        /// Consente di popolare le proprieta' in binding con il Panorama.
        /// </summary>
        public async void PopolaPanoramaProperties()
        {
            // Pivot 2
            var listSchedineFromDb = await dataAccessDb.GetSchedine();
            foreach (Schedina s in listSchedineFromDb)
            {
                if (!Schedine.Contains(s))
                {
                    Schedine.Add(s);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot 2.
        /// </summary>
        public ObservableCollection<Schedina> Schedine
        {
            get 
            {
                System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "GET Schedine");
                return schedine; 
            }

            set
            {
                if (schedine != value)
                {
                    schedine = value;
                    System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "SET Schedine");
                    RaisePropertyChanged(() => Schedine);
                }
            }
        }

        /// <summary>
        /// Proprieta' in binding con la casella di testo nel Pivot 1.
        /// </summary>
        public Schedina NuovaSchedina
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "GET NuovaSchedina");
                return nuovaSchedina;
            }

            set
            {
                if (nuovaSchedina != value)
                {
                    nuovaSchedina = value;
                    System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "SET NuovaSchedina");
                    RaisePropertyChanged(() => NuovaSchedina);
                }
            }
        }

        /// <summary>
        /// RelayCommand per la Navigation.
        /// </summary>
        public RelayCommand<Schedina> NavToPageCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per la cancellazione di un elemento Schedina dalla lista.
        /// </summary>
        public RelayCommand<Schedina> DeleteSchedinaCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// RelayCommand per eseguire il refresh delle proprieta' in binding.
        /// </summary>
        public RelayCommand RefreshBindingData
        {
            get;
            private set;
        }
    }
}
