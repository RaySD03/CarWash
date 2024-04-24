using CarWash.Models;

namespace CarWash.Views;

public partial class SelectVehicles : ContentPage
{
	public SelectVehicles()
	{
        BindingContext = new Car();
		InitializeComponent();
        CarList_Services.Cars.Clear();
        CarListCollectionView.ItemsSource = Car.Cars;
	}
    public void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = sender as CheckBox;
        var selected = checkbox.BindingContext as Car;
        var car = new CarList_Services { Identifier = selected.Identifier, Make = selected.Make, Model = selected.Model, Color = selected.Color, Year = selected.Year, Service = "TBD" };

        // Add or remove checked cars from the collectionview for the services list
        if (checkbox.IsChecked == true)
        {        
            CarList_Services.Cars.Add(car);
        }
        else
        {
            CarList_Services.Cars.Remove(CarList_Services.Cars.Where(i => i.Identifier == selected.Identifier).Single());
        }
    }

    public async void gotoSpecifyServices(object sender, EventArgs e) 
	{
        if (CarList_Services.Cars.Count > 0)
        {
            await Navigation.PushAsync(new ServicesSelection());
        }
        else
        {
            await DisplayAlert("Error", "Please select at least one car to continue", "OK");
        }
	}
}