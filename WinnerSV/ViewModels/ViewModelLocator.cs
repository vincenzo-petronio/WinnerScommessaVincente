//-----------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs">
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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using WinnerSV.DataModel;

namespace WinnerSV.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            // IoC CONTAINER
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                ////SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                ////SimpleIoc.Default.Register<IDataService, DataService>();

                // DATA ACCESS - SERVICE AGENT
                SimpleIoc.Default.Register<IDataAccessDb, DataAccessDb>(true);
                SimpleIoc.Default.Register<IServiceAgent, ServiceAgentSports>(true);
            }

            // VIEW MODELS
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SportsViewModel>();
            SimpleIoc.Default.Register<AnteprimaSchedinaViewModel>();
        }

        /// <summary>
        /// ViewModel Panorama
        /// </summary>
        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        /// <summary>
        /// ViewModel Sports
        /// </summary>
        public SportsViewModel Sports
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SportsViewModel>(Guid.NewGuid().ToString());
            }
        }

        /// <summary>
        /// ViewModel AnteprimaSchedina
        /// </summary>
        public AnteprimaSchedinaViewModel AnteSchedina
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnteprimaSchedinaViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
