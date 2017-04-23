using Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigation.Helpers;
using Xamarin.Forms;
using Navigation.ViewModels;
using Navigation.Views;

namespace Navigation.Services
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// DICTIONARY THAT MAPS VIEWMODELS TO THEIR SPECIFIC VIEWS
        /// </summary>
        protected static readonly Dictionary<Type, Type> Mappings = new Dictionary<Type, Type>();

        /// <summary>
        /// PROVIDES ACCESS TO THE APPLICATION ROOT NAVIGATION INTERFACE
        /// + ABSTRACTION PROVIDED BY XAMARIN FORMS TO HIDE PLATFORM SPECIFIC NAVIGATION
        /// </summary>
        private static INavigation _navigation;
        protected static INavigation Navigation
        {
            get { return _navigation; }
        }

        /// <summary>
        /// CONSTRUCTOR: INITIALIZES VIEWMODEL-VIEW MAPPINGS
        /// </summary>
        public NavigationService()
        {
            LoadPageViewModelMappings();
        }

        private void LoadPageViewModelMappings()
        {
            Mappings.Add(typeof(WelcomeViewModel), typeof(WelcomeView));
            Mappings.Add(typeof(MainViewModel), typeof(MainView));
        }
        
        #region CONTRACT IMPLEMENTATION
        /** THIS REGION PROVIDES THE IMPLEMENTATION FOR THE NAVIGATION SERVICE  
         *  FAIR WARNING: THE LOGIC CAN BE CONFUSING
         *  
         *  THE INTERFACE PROVIDES DIFFERENT WAYS OF NAVIGATING TO A VIEW
         *  INTERNALLY THE SERVICE USES AN INTERNAL NAVIGATION FUNCTION THAT IS
         *  CALLED ACCORDINGLY  
         * */

        /// <summary>
        /// INITIALIZES THE NAVIGATION SERVICE
        /// </summary>
        public Task InitializeAsync(object Parameter)
        {
            _navigation = Parameter as INavigation;
            return NavigateToAsync<WelcomeViewModel>();
        }

        /// <summary>
        /// NAVIGATE TO A VIEW - RESOLVED BY THE VIEWMODEL TYPE
        /// </summary>
        public Task NavigateToAsync<TViewModel>(object Parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), Parameter);
        }
        
        /// <summary>
        /// NAVIGATE TO A VIEW - RESOLVED BY THE VIEWMODEL TYPE PASSED AS A PARAMETER
        /// </summary>
        public Task NavigateToAsync(Type ViewModelType, object Parameter = null)
        {
            return InternalNavigateToAsync(ViewModelType, Parameter);
        }
        
        /// <summary>
        /// NAVIGATE BACK FROM A VIEW
        /// </summary>
        public async Task NavigateBackAsync()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region INTERNAL OPERATIONS
        /// <summary>
        /// GETS A VIEW BOUND TO A VIEWMODEL VIA A FUNCTION
        /// PERFORMS THE RAW NAVIGATION
        /// INITIALIZES THE VIEWMODEL WITH PARAMETER (IF ANY)
        /// </summary>
        protected virtual async Task InternalNavigateToAsync(Type ViewModel, object Parameter)
        {
            Page P = CreateAndBindPage(ViewModel, Parameter);
            await Navigation.PushAsync(P);

            await (P.BindingContext as ViewModelBase).InitializeAsync(Parameter);
        }

        /// <summary>
        /// CREATES AN INSTANCE OF THE VIEW
        /// CREATES AND BIND AN INSTANCE OF THE VIEWMODEL
        /// </summary>
        protected Page CreateAndBindPage(Type ViewModel, object Parameter)
        {
            Type PageType = GetPageTypeForViewModel(ViewModel);

            if (PageType == null)
                throw new Exception($"Page for {ViewModel} could not be found.");

            Page Target = Activator.CreateInstance(PageType) as Page;
            ViewModelBase ViewModelInstance = ResourceLocator.Instance.Resolve(ViewModel) as ViewModelBase;
            Target.BindingContext = ViewModelInstance;

            return Target;
        }

        /// <summary>
        /// RETURNS THE VIEW MAPPED TO A VIEWMODEL
        /// </summary>
        private static Type GetPageTypeForViewModel(Type ViewModel)
        {
            if (!Mappings.ContainsKey(ViewModel))
                throw new KeyNotFoundException($"Page for ${ViewModel} could not be found.");

            return Mappings[ViewModel];
        }
        #endregion
    }
}