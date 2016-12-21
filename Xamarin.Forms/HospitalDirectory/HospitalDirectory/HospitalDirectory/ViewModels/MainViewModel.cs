using HospitalDirectory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HospitalDirectory.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedPropertyChanged(String info)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public MainViewModel()
        {

        }

        public string _hospitalname;

        public string HospitalName
        {
            get { return _hospitalname; }
            set
            {
                _hospitalname = value;
                RaisedPropertyChanged("HospitalName");
            }
        }

        private string _hlocation;

        public string HLocation
        {
            get { return _hlocation; }
            set
            {
                _hlocation = value;
                RaisedPropertyChanged("HLocation");
            }
        }


        private ObservableCollection<Hospital> _hospitallist = new ObservableCollection<Hospital>();
        public ObservableCollection<Hospital> HospitalList
        {
            get { return _hospitallist; }
            set
            {
                _hospitallist = value;

                RaisedPropertyChanged("HospitalList");
            }
        }

        public ICommand AddEntry
        {
            get
            {

                return new Command(() =>
                {

                    var newentry = new Hospital { Location = HLocation, Name = HospitalName };

                    HospitalList.Add(newentry);
                });

            }
        }

    }


}
