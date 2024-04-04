using CarWash.Models;
using Google.Cloud.Firestore;

namespace CarWash.Views;

public partial class ManageMyCars : ContentPage
{
	public ManageMyCars()
	{
		InitializeComponent();
        BindingContext = new Garage();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        CarListCollectionView.ItemsSource = Garage.Cars;
        getCarList();
    }

    public async void getCarList()
    {
       
        if (Garage.Cars.Count == 0)
        {
            Label.Text = "Tap the + button to add a car.";
            CarListCollectionView.IsVisible = false;
            addBtn.IsVisible = true;
        }
        else
        {
            Label.Text = Garage.Cars.Count + " / 5 cars";
            CarListCollectionView.IsVisible = true;

            if (Garage.Cars.Count == 5)
                addBtn.IsVisible = false;
        }
    }
    public void Remove_Selected_Car(object sender, EventArgs e)
    {
        var button = sender as Button;
        var car = button.BindingContext as Garage;
        var vm = BindingContext as Garage;
        vm.RemoveCommand.Execute(car);
        Label.Text = Garage.Cars.Count + " / 5 cars";

        var email = Preferences.Get("UserEmail", "");

        FirestoreDb db = FirestoreDb.Create("carwash-da88f");
        DocumentReference docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").Document(car.Identifier);
        docRef.DeleteAsync();

        if (Garage.Cars.Count < 5) 
        {
            addBtn.IsVisible = true;
        }

    }
    public async void goToVehicleAddition(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAdditionPage());
    }
    public async void returnHome(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}