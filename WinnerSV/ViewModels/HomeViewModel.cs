using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        /// <summary>
        /// Costruttore.
        /// </summary>
        public HomeViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
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
    }
}
