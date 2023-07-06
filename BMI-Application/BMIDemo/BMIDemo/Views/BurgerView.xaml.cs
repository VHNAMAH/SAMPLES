using BMIDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BMIDemo.Views
{
    public partial class BurgerView : ContentPage
    {
        private BurgerViewModel _viewModel;
        public BurgerView()
        {
            InitializeComponent();

            _viewModel = new BurgerViewModel(Navigation);
            BindingContext = _viewModel;
        }
    }
}
