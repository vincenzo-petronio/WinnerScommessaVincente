using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnerSV.DataModel;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// ViewModel associato al controllo che include l'anteprima della schedina.
    /// </summary>
    public class AnteprimaSchedinaViewModel : ViewModelBase
    {
        private IDataAccessDb dataAccessDb;
        private Schedina selectedSchedina;

        public AnteprimaSchedinaViewModel(IDataAccessDb db)
        {
            if (IsInDesignMode)
            {
                // design-mode
            }
            else
            {
                this.dataAccessDb = db;

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

        private async void SetSelectedSchedina()
        {
            Schedina s = await dataAccessDb.GetSchedina("Nome Cognome 110");
            SelectedSchedina = s;
        }

    }
}
