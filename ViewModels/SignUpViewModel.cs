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
using Google.Cloud.Firestore;

namespace CarWash.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {

        public string webApiKey = "AIzaSyAaQodvzegxflBMymuDyGo4fZ67SXiDZ_4";

        private SignUpRequestModel mySignUpRequestModel = new SignUpRequestModel();
        public SignUpRequestModel MySignUpRequestModel
        { 
            get { return mySignUpRequestModel; }
            set { mySignUpRequestModel = value;

                OnPropertyChanged(nameof(MySignUpRequestModel));
            }
        }
        public ICommand SignUpProcedure {  get;}
        public SignUpViewModel() 
        {
            SignUpProcedure = new Command(PerformSignUp);
        }

        private async void PerformSignUp(object obj)
        {
            // Firebase Authentication here
            var credentials = mySignUpRequestModel;
            Preferences.Set("FreshFirebaseToken", null);
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                if (credentials != null && credentials.Password == credentials.ConfirmPassword)
                {
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(credentials.Email, credentials.Password);

                    // Add user's fullname
                    FirestoreDb db = FirestoreDb.Create("carwash-da88f");
                    var user = db.Collection("users").Document(credentials.Email.ToString());
                    await user.CreateAsync(new { Fullname = credentials.Name.ToString()});

                    await App.Current.MainPage.DisplayAlert("Message","Sign up was successful.\nPlease login with your credentials.", "OK");
                }
                else
                {
                    return;
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
