using Navigation.Interfaces;
using Navigation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Helpers
{
    public class ViewModelBase : PropertyChanged
    {
        /// <summary>
        /// PROVIDES ACCESS TO THE NAVIGATION SERVICE
        /// </summary>
        protected readonly INavigationService Navigation;

        /// <summary>
        /// STORES ANY PARAMETER PASSED DURING CREATION OF THE VIEWMODEL
        /// </summary>
        private object _parameter;

        public object Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                RaisePropertyChanged(() => Parameter);
            }
        }

        /// <summary>
        /// THE STATE OF THE VIEW MODEL
        /// </summary>
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            Navigation = ResourceLocator.Instance.Resolve<INavigationService>();
            Parameter = null;
        }

        public virtual Task InitializeAsync(object Data = null)
        {
            if (Data != null) Parameter = Data;
            return Task.FromResult(false);
        }
    }
}