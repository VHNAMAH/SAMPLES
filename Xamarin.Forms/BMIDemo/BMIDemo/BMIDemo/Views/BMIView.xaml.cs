using BMIDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BMIDemo.Views
{
    public partial class BMIView : ContentPage
    {
        private BMIViewModel _viewModel;

        public BMIView()
        {
            InitializeComponent();

            _viewModel = new BMIViewModel();
            BindingContext = _viewModel;
        }
    }
}
