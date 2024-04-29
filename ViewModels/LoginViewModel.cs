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
using Microsoft.Maui.ApplicationModel.Communication;
using Google.Api;

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
        public ICommand PasswordReset { get; }
        public LoginViewModel() 
        {
            LoginProcedure = new Command(PerformLogin);
            PasswordReset = new Command(ForgotPassword);
        }

        private async void ForgotPassword(object obj)
        {  
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            string email = await App.Current.MainPage.DisplayPromptAsync("Forgot Password", "What's the email address?", initialValue: "", maxLength: 40, keyboard: Keyboard.Email);
            
            try
            {
                if (email != null)
                {
                    //authProvider.SendPasswordResetEmailAsync(email);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }
        private async void PerformLogin(object obj)
        {
            // Firebase Authentication here
            var credentials = myLoginRequestModel;
            Preferences.Set("FreshFirebaseToken", null);
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                if (credentials.Email != null && credentials.Password != null)
                {
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(credentials.Email, credentials.Password);
                    var content = await auth.GetFreshAuthAsync();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    Preferences.Set("UserEmail", "");
                    Preferences.Set("Fullname", "");

                    Preferences.Set("IsLoggedIn", true);
                    Preferences.Set("UserEmail", credentials.Email);

                    FirestoreDb db = FirestoreDb.Create("carwash-da88f");           
                    DocumentSnapshot retrieved = await db.Collection("users").Document(credentials.Email).GetSnapshotAsync(); ;

                    foreach (KeyValuePair<string, object> pair in retrieved.ToDictionary())
                    {
                        if (pair.Key == "Fullname")
                        {
                            Preferences.Set("Fullname", pair.Value.ToString());
                        }
                    }             
                    
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
