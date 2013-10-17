using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using WinnerSV.Helpers;
using WinnerSV.ViewModels;

namespace WinnerSV.Views
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();

            // MESSENGER per la Navigation.
            Messenger.Default.Register<NavToPage>(this, (action) => ReceiveMessage(action));
        }

        /// <summary>
        /// Proprieta' per accedere al View-Model dal Code-Behind
        /// </summary>
        private HomeViewModel VM
        {
            get
            {
                return (HomeViewModel)this.DataContext;
            }
        }

        private object ReceiveMessage(NavToPage action)
        {
            if (action != null)
            {
                var page = string.Format("/Views/{0}.xaml", action.PageName);

                if (action.PageParameter != WinnerSV.Resources.AppResources.PanoramaPivot1ItemTitle)
                {
                    // QueryString
                    page += "?Title=" + action.PageParameter;
                }

                NavigationService.Navigate(new System.Uri(page, System.UriKind.Relative));
            }
            return null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.VM.PopolaPanoramaProperties();
        }
    }
}