using CarWash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginRequestModel myLoginRequestModel = new LoginRequestModel();
        public LoginRequestModel MyLoginRequestModel 
        { 
            get { return myLoginRequestModel; }
            set { myLoginRequestModel = value;

                OnPropertyChanged(nameof(MyLoginRequestModel));
            }
        }

        public ICommand LoginProcedure {  get;}
        public LoginViewModel() 
        {
            LoginProcedure = new Command(PerformLogin);
        }

        private async void PerformLogin(object obj)
        {
            // Firebase Authentication here
            var credentials = myLoginRequestModel;

            Preferences.Set("IsLoggedIn", true);
            await Shell.Current.GoToAsync(state: "//UserProfile");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
