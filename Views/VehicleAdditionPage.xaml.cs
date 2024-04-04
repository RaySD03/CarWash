using CarWash.Models;
using Google.Cloud.Firestore;
using System;

namespace CarWash.Views;

public partial class VehicleAdditionPage : ContentPage
{
	public VehicleAdditionPage()
	{
		InitializeComponent();
	}
    public async void addCar(object sender, EventArgs e)
    {
        if (Garage.Cars.Count < 5)
        {
            var car = new Garage { Identifier = Garage.Cars.Last().Identifier + 1, Make = makeEntry.Text, Model = modelEntry.Text, Year = yearEntry.Text, Color = ColorPicker.SelectedItem.ToString(), Icon = "car_list_icon.png" };
            Garage.Cars.Add(car);

            var email = Preferences.Get("UserEmail", "");

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            string carid = car.Identifier.ToString();
            DocumentReference docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").Document(carid);
            docRef.CreateAsync(new { Make= car.Make, Model= car.Model, Year= car.Year, Color= car.Color });

            Console.WriteLine("Car added successfully");

            this.Navigation.PopAsync();
        }
        else
        {
           
        }     
    }
}