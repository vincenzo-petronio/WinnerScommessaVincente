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
    /// Vista associata all'incontro selezionato, contenente tutte le possibili
    /// quote previste.
    /// </summary>
    public class IncontroViewModel : ViewModelBase
    {
        private Incontro datiIncontro;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public IncontroViewModel()
        {
            if (IsInDesignMode)
            {
                // design-mode
            }
            else
            {

            }
        }

        /// <summary>
        /// Oggetto contenente tutti i dati della scommessa relativi ad un incontro.
        /// </summary>
        public Incontro DatiIncontro
        {
            get
            {
                return datiIncontro;
            }

            set
            {
                if (datiIncontro != value)
                {
                    datiIncontro = value;
                    RaisePropertyChanged(() => DatiIncontro);
                }
            }
        }
    }
}
