using CarWash.Models;
using Google.Cloud.Firestore;

namespace CarWash.Views;

public partial class ManageMyCars : ContentPage
{
	public ManageMyCars()
	{
		InitializeComponent();
        BindingContext = new Car();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarListCollectionView.ItemsSource = Car.Cars;
        getCarList();
    }

    public void getCarList()
    {
       
        if (Car.Cars.Count == 0)
        {
            Label.Text = "Tap the + button to add a car.";
            CarListCollectionView.IsVisible = false;
            addBtn.IsVisible = true;
        }
        else
        {
            Label.Text = Car.Cars.Count + " / 5 cars";
            CarListCollectionView.IsVisible = true;

            if (Car.Cars.Count == 5)
                addBtn.IsVisible = false;
        }
    }
    public void Remove_Selected_Car(object sender, EventArgs e)
    {
        var button = sender as Button;
        var car = button.BindingContext as Car;
        var vm = BindingContext as Car;
        // Remove car from local ObservableCollection
        vm.RemoveCommand.Execute(car);

        // Update car count label
        Label.Text = Car.Cars.Count + " / 5 cars";

        // Remove car from database
        var email = Preferences.Get("UserEmail", "");
        FirestoreDb db = FirestoreDb.Create("carwash-da88f");
        DocumentReference docRef = db.Collection("users").Document(email.ToString()).Collection("CarList").Document(car.Identifier);
        docRef.DeleteAsync();

        // If car count is < 5 after removing a car, make the addBtn visible
        if (Car.Cars.Count < 5) 
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