using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WinnerSV.ViewModels;
using System;
using WinnerSV.Common;

namespace WinnerSV.Views
{
    public partial class SchedinaView : PhoneApplicationPage
    {
        public SchedinaView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Proprieta' per accedere al ViewModel dal Code-Behind
        /// </summary>
        private AnteprimaSchedinaViewModel VM
        {
            get
            {
                return (AnteprimaSchedinaViewModel)this.DataContext;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Aggiorno AnteprimaSchedinaViewModel con il titolo della Schedina, a seconda se e'
            // una nuova schedina o un caricamento di una schedina gia' giocata.
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("Title", out parameter))
            {
                this.VM.SelectedSchedina.Title = parameter;
            }
            else
            {
                DateTime dt = DateTime.Now;
                var dtFormatted = String.Format("{0:g}", dt);
                this.VM.SelectedSchedina.Title = (Constants.TITLE_SCHEDINA_DEFAULT + " " + dtFormatted).Replace(" ", "_");
            }

            this.VM.PopolaAnteprimaProperties();
        }
    }
}