using CarWash.Models;
using Google.Cloud.Firestore;
using System;

namespace CarWash.Views;

public partial class VehicleAdditionPage : ContentPage
{
	public VehicleAdditionPage()
	{
		InitializeComponent();
        BindingContext = Car.Cars;
	}
    public async void addCar(object sender, EventArgs e)
    {
        if (Car.Cars.Count < 5)
        {
            var car = new Car { Identifier = "", Make = makeEntry.Text, Model = modelEntry.Text, Year = yearEntry.Text, Color = ColorPicker.SelectedItem.ToString(), Icon = "car_list_icon.png" };
            
            try 
            {           
                var email = Preferences.Get("UserEmail", "");

                FirestoreDb db = FirestoreDb.Create("carwash-da88f");
                DocumentReference docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").Document();
                docRef.CreateAsync(new { Make = makeEntry.Text, Model = modelEntry.Text, Year = yearEntry.Text, Color = ColorPicker.SelectedItem.ToString()});

                car.Identifier = docRef.Id.ToString();
                Car.Cars.Add(car);
                await this.Navigation.PopAsync();
            } 
            catch (Exception ex) 
            {

            }
        }
        else
        {
           
        }     
    }
}