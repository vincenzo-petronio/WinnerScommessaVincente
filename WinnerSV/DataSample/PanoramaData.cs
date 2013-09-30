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
    public class PanoramaData : ViewModelBase
    {
        private ObservableCollection<Schedina> schedine;

        public PanoramaData()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                schedine = new ObservableCollection<Schedina>
                {
                    new Schedina
                    {
                        Title = "DM - Lorem ipsum dolor sit amet",
                    },
                    new Schedina 
                    {
                        Title = "DM - Maecenas et massa dapibus",
                    },
                    new Schedina 
                    {
                        Title = "DM - Fusce nec turpis ut ligula",
                    },
                    new Schedina 
                    {
                        Title = "DM - Nunc condimentum, nunc eget volutpat tempor, turpis purus lobortis arcu",
                    }
                };
            }
        }

        public ObservableCollection<Schedina> Schedine
        {
            get { return schedine; }
        }
    }
}
