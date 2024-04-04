using CarWash.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.Collections.ObjectModel;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;


namespace CarWash.Views;
public partial class HomeScreen : ContentPage
{
    public HomeScreen()
	{
		InitializeComponent();
        BindingContext = new Garage();
        Garage.Cars.Add(new Garage { Identifier = 0, Make = "BMW", Model = "Turbo", Year = "2002", Color = "Orange", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Identifier = 1, Make = "Honda", Model = "Accord", Year = "2018", Color = "White", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Identifier = 2, Make = "Hyundai", Model = "Elantra", Year = "2022", Color = "Blue", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Identifier = 3, Make = "Volkswagen", Model = "Golf", Year = "2019", Color = "Silver", Icon = "car_list_icon.png" });
        getCarList();

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        CarListCollectionView.ItemsSource = Garage.Cars;

        string filepath = "C:\\Users\\aniks\\OneDrive\\Desktop\\application_default_credentials.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);

        FirestoreDb db = FirestoreDb.Create("carwash-da88f");
        // Create a document with a random ID in the "users" collection.
        //CollectionReference collection = db.Collection("users");
        //DocumentReference document = await collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = 1815 });
    }
    private void getCarList()
	{
        CarListCollectionView.ItemsSource = Garage.Cars;          
    }
	public async void goToSchedule(object sender, EventArgs e)
	{
        if (Garage.Cars.Count > 0)
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