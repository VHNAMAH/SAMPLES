using Navigation.Helpers;
using Navigation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private User _master = new User();

        public User Master
        {
            get { return _master; }
            set
            {
                _master = value;
                RaisePropertyChanged(() => Master);
            }
        }

        public ICommand NavigateToMain
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<MainViewModel>(Master);
                });
            }
        }
    }
}