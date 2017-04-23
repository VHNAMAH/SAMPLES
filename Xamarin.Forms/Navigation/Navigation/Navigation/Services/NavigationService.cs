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
        /// MAPS VIEWMODELS TO THEIR SPECIFIC VIEWS
        /// </summary>
        protected static readonly Dictionary<Type, Type> Mappings = new Dictionary<Type, Type>();

        /// <summary>
        /// PROVIDES ACCESS TO THE APPLICATION ROOT FOR NAVIGATION
        /// </summary>
        protected static Application Current
        {
            get { return Application.Current; }
        }

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

        #region SERVICE IMPLEMENTATION
        public Task InitializeAsync(object Parameter)
        {
            _navigation = Parameter as INavigation;
            return NavigateToAsync<WelcomeViewModel>();
        }

        public Task NavigateToAsync<TViewModel>(object Parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), Parameter);
        }

        public Task NavigateToAsync(Type ViewModelType, object Parameter = null)
        {
            return InternalNavigateToAsync(ViewModelType, Parameter);
        }

        public async Task NavigateBackAsync()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region INTERNAL OPERATIONS
        protected virtual async Task InternalNavigateToAsync(Type ViewModel, object Parameter)
        {
            Page P = CreateAndBindPage(ViewModel, Parameter);
            await Navigation.PushAsync(P);

            await (P.BindingContext as ViewModelBase).InitializeAsync(Parameter);
        }

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

        private static Type GetPageTypeForViewModel(Type ViewModel)
        {
            if (!Mappings.ContainsKey(ViewModel))
                throw new KeyNotFoundException($"Page for ${ViewModel} could not be found.");

            return Mappings[ViewModel];
        }
        #endregion
    }
}