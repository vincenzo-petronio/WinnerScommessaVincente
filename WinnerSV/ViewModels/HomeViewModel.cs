using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnerSV.DataModel;
using WinnerSV.DataSample;

namespace WinnerSV.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<Schedina> schedine;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public HomeViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                // TODO Fare injection con IDataService
                PanoramaData pd = new PanoramaData();
                schedine = pd.Schedine;
            }
            else
            {
                // Code runs "for real"
                NavToPageCommand = new RelayCommand(() =>
                {
                    System.Diagnostics.Debug.WriteLine("[HOMEVIEWMODEL] " + "Tapped NavToPageCommand!");
                });
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
        /// Proprieta' in binding con la lista presente nel Pivot 2.
        /// </summary>
        public ObservableCollection<Schedina> Schedine
        {
            get { return schedine; }
        }
    }
}
