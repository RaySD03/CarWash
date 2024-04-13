using CarWash.Models;
using Google.Cloud.Firestore;
using System;
using System.Threading.Tasks;


namespace CarWash.Views;

public partial class VehicleAdditionPage : ContentPage
{
    public VehicleAdditionPage()
    {
        InitializeComponent();
        BindingContext = Car.Cars;
    }

    public async void AddCar(object sender, EventArgs e)
    {
        if (Car.Cars.Count >= 5)
        {
            // Notify the user that no more cars can be added
            await DisplayAlert("Limit Reached", "You cannot add more than 5 cars.", "OK");
            return;
        }

        if (!IsInputValid())
        {
            await DisplayAlert("Invalid Input", "Please check your inputs and try again.", "OK");
            return;
        }

        var car = new Car
        {
            Make = makeEntry.Text,
            Model = modelEntry.Text,
            Year = yearEntry.Text,
            Color = ColorPicker.SelectedItem.ToString(),
            Icon = "car_list_icon.png"
        };

        try
        {
            var email = Preferences.Get("UserEmail", string.Empty);
            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            DocumentReference docRef = await db.Collection("users").Document(email).Collection("CarList").AddAsync(car);
            car.Identifier = docRef.Id;

            Car.Cars.Add(car);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add car: {ex.Message}", "OK");
        }
    }

    private bool IsInputValid()
    {
        // Implement validation logic here
        return !string.IsNullOrWhiteSpace(makeEntry.Text) &&
               !string.IsNullOrWhiteSpace(modelEntry.Text) &&
               !string.IsNullOrWhiteSpace(yearEntry.Text) &&
               ColorPicker.SelectedItem != null;
    }
}