using GalaSoft.MvvmLight;
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

    public class SportsViewModel : ViewModelBase
    {

        private ObservableCollection<Calcio> listCalcio;
        private ObservableCollection<Basket> listBasket;
        private ObservableCollection<Tennis> listTennis;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public SportsViewModel()
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
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Calcio.
        /// </summary>
        public ObservableCollection<Calcio> ListCalcio
        {
            get { return listCalcio; }
        }
        
        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Basket.
        /// </summary>
        public ObservableCollection<Basket> ListBasket
        {
            get { return listBasket; }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Tennis.
        /// </summary>
        public ObservableCollection<Tennis> ListTennis
        {
            get { return listTennis; }
        }
    }
}
