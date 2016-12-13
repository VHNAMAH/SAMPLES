using BMIDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BMIDemo.ViewModels
{
    public class BurgerViewModel
    {
        //VARIABLE: ALLOWS NAVIGATION
        private INavigation _navigator;

        public ICommand GotoAboutView
        {
            get
            {
                return new Command(async () =>
                {
                    //THIS PIECE OF WEIRD CODE IS ACTUALLY JUST A FUNCTION
                    //INSTEAD OF DOING AS ABOVE WITH CALCULATE BMI
                    //I.E. WRITING A SEPARATE FUNCTION THEN LINKING IT TO THE ICOMMAND
                    //HERE THE WHOLE FUNCTION IS WRITTEN DIRECTLY AS A PARAMETER

                    await _navigator.PushAsync(new AboutView());
                });
            }
        }

        public BurgerViewModel(INavigation Navigator)
        {
            _navigator = Navigator;
        }
    }
}
