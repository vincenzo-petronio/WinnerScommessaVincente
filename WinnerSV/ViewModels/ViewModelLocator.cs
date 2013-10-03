using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
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
            }

            // VIEW MODELS
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SportsViewModel>();
            SimpleIoc.Default.Register<AnteprimaSchedinaViewModel>();
            SimpleIoc.Default.Register<IncontroViewModel>();
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
                return ServiceLocator.Current.GetInstance<SportsViewModel>();
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

        /// <summary>
        /// ViewModel Incontro
        /// </summary>
        public IncontroViewModel Incontro
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IncontroViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
