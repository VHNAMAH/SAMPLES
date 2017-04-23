using Navigation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// INITIALIZES THE NAVIGATION SERVICE
        /// </summary>
        Task InitializeAsync(object Parameter);
        
        /// <summary>
        /// NAVIGATE TO A VIEW - RESOLVED BY THE VIEWMODEL TYPE
        /// </summary>
        Task NavigateToAsync<TViewModel>(object Parameter = null) where TViewModel : ViewModelBase;
        
        /// <summary>
        /// NAVIGATE TO A VIEW - RESOLVED BY THE VIEWMODEL TYPE PASSED AS A PARAMETER
        /// </summary>
        Task NavigateToAsync(Type ViewModelType, object Parameter = null);

        /// <summary>
        /// NAVIGATE BACK FROM A VIEW
        /// </summary>
        Task NavigateBackAsync();
    }
}