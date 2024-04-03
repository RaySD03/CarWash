using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarWash.ViewModels
{
    public class UserProfileViewModel
    {
        public ICommand Logout {  get; }
        public UserProfileViewModel() 
        {
            Logout = new Command(PerformLogout);
        }

        private async void PerformLogout(object obj)
        {
            Preferences.Clear();
            Preferences.Set("IsLoggedIn", false);
            await Shell.Current.GoToAsync("//Login");
        }
    }
}
