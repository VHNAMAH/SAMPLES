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
        Task InitializeAsync(object Parameter);
        
        Task NavigateToAsync<TViewModel>(object Parameter = null) where TViewModel : ViewModelBase;
        
        Task NavigateToAsync(Type ViewModelType, object Parameter = null);

        Task NavigateBackAsync();
    }
}
