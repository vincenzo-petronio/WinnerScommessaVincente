﻿//-----------------------------------------------------------------------
// <copyright file="SchedinaView.xaml.cs">
//    Copyright (c) 2013 Vincenzo Petronio <vincenzo.petronio"AT"gmail.com>. All rights reserved.
// </copyright>
// <license>
//    This file is part of Winner - Scommessa vincente.
//
//    Winner - Scommessa vincente is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 2 of the License, or
//    (at your option) any later version.
//
//    Winner - Scommessa vincente is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Winner - Scommessa vincente.  If not, see <http://www.gnu.org/licenses/>.
// </license>
//-----------------------------------------------------------------------
       
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