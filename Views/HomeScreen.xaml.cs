using CarWash.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;


namespace CarWash.Views;
public partial class HomeScreen : ContentPage
{
    public HomeScreen()
	{
        InitializeComponent();
        //Garage.Cars.Add(new Garage { Identifier = "0", Make = "BMW", Model = "Turbo", Year = "2002", Color = "Orange", Icon = "car_list_icon.png" });
        //Garage.Cars.Add(new Garage { Identifier = "1", Make = "Honda", Model = "Accord", Year = "2018", Color = "White", Icon = "car_list_icon.png" });
        //Garage.Cars.Add(new Garage { Identifier = "2", Make = "Hyundai", Model = "Elantra", Year = "2022", Color = "Blue", Icon = "car_list_icon.png" });
        //Garage.Cars.Add(new Garage { Identifier = "3", Make = "Volkswagen", Model = "Golf", Year = "2019", Color = "Silver", Icon = "car_list_icon.png" });
        BindingContext = new Car();    

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        CarListCollectionView.ItemsSource = Car.Cars;
        getCarList();
    }
    public async Task getCarList()
	{
        Car.Cars.Clear();

        try
        {        
            var email = Preferences.Get("UserEmail", "");
            var vm = BindingContext as Car;

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").GetSnapshotAsync();

            await Task.Run(async () =>
            {
                foreach (DocumentSnapshot doc in await docRef.ConfigureAwait(false))
                {
                    var car = new Car { Identifier = "", Make = "", Model = "", Year = "", Color = "Silver", Icon = "car_list_icon.png" };
                    DocumentSnapshot retrieved_car = await db.Collection("users").Document(email.ToString()).Collection("CarList").Document(doc.Id).GetSnapshotAsync();
               
                    foreach (KeyValuePair<string, object> pair in retrieved_car.ToDictionary())
                    {
                        if (pair.Key == "Make")
                        {
                            car.Make = (string)pair.Value;
                        }
                        if (pair.Key == "Model")
                        {
                            car.Model = (string)pair.Value;
                        }
                        if (pair.Key == "Year")
                        {
                            car.Year = (string)pair.Value;
                        }
                        if (pair.Key == "Color")
                        {
                            car.Color = (string)pair.Value;
                        }
                    }
                    // Set identifier of each car which is the docID from the db
                    car.Identifier = doc.Id;
                    Car.Cars.Add(car);
                }
            });
        }
        catch (Exception ex) 
        { 
        
        }
    }
	public async void goToSchedule(object sender, EventArgs e)
	{
        if (Car.Cars.Count > 0)
        {
            await Navigation.PushAsync(new Schedule());
        }
		else
        {
            await DisplayAlert("Error", "Please add at least one car to continue", "OK");
        }
	}
    public async void goToManageCars(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageMyCars(), true);
    }
  
}