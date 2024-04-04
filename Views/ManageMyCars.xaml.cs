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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        getCarList();
    }
    private void getCarList()
    {
        CarListCollectionView.ItemsSource = Garage.Cars;

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
        string carid = car.Identifier.ToString();
        DocumentReference docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").Document(car.Identifier.ToString());
        docRef.DeleteAsync();

        int i = 0;
        foreach (var entry in Garage.Cars)
        {
            entry.Identifier = i;
            i++;
        }

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