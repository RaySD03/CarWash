using CarWash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Firebase.Auth;
using CarWash.Views;

namespace CarWash.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyAaQodvzegxflBMymuDyGo4fZ67SXiDZ_4";

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
            Preferences.Set("FreshFirebaseToken", null);
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                if (credentials != null)
                {
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(credentials.Email, credentials.Password);
                    var content = await auth.GetFreshAuthAsync();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    Preferences.Set("UserEmail", "");    

                    Preferences.Set("IsLoggedIn", true);
                    Preferences.Set("UserEmail", credentials.Email);
                    Car.Cars.Clear();
                    Appointment.MyAppointments.Clear();
                    await Shell.Current.GoToAsync("//UserProfile");
                }             
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }         
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
