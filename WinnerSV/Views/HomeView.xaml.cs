using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Messaging;
using WinnerSV.Helpers;

namespace WinnerSV.Views
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();

            // MESSENGER
            Messenger.Default.Register<NavToPage>(this, (action) => ReceiveMessage(action));
        }

        private object ReceiveMessage(NavToPage action)
        {
            if (action != null)
            {
                var page = string.Format("/Views/{0}.xaml", action.PageName);

                ////if (action.PageName == "Main")
                ////{
                ////    page = "/MainPage.xaml";
                ////}
                NavigationService.Navigate(new System.Uri(page, System.UriKind.Relative));
            }
            return null;
        }
    }
}