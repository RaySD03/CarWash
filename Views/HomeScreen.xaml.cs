using CarWash.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
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
        Car.Cars.Add(new Car { Identifier = "0", Make = "BMW", Model = "Turbo", Year = "2002", Color = "Orange", Icon = "car_list_icon.png" });
        
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();    
        getCarList();
    }
    public async Task getCarList()
	{
        CarListCollectionView.ItemsSource = Car.Cars;
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