using HospitalDirectory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HospitalDirectory.Views
{
    public partial class MainView : ContentPage
    {
        private MainViewModel _vm;
       
        public MainView()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            BindingContext = _vm;
        }
    }
}
