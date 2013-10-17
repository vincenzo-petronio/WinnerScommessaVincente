using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinnerSV.ViewModels;
using WinnerSV.DataModel;

namespace WinnerSV.Views
{
    public partial class IncontroView : PhoneApplicationPage
    {
        public IncontroView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ////base.OnNavigatedTo(e);

            ////var pageDataContext = (AnteprimaSchedinaViewModel)this.DataContext;

            ////var gridDataContext = (Incontro)this.ContentPanel.DataContext;

            ////pageDataContext.SelectedIncontro = gridDataContext;
        }
    }
}