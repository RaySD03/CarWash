using CarWash.Models;

namespace CarWash.Views;

public partial class SelectVehicles : ContentPage
{
	public SelectVehicles()
	{
		InitializeComponent();
        CarList_Selected.Cars.Clear();
        CarListCollectionView.ItemsSource = Garage.Cars;
	}
    public void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = sender as CheckBox;
        var selected = checkbox.BindingContext as Garage;
        var car = new CarList_Selected { Make = selected.Make, Model = selected.Model, Color = selected.Color, Year = selected.Year, Service = "" };

        // Add or remove checked cars from the collectionview to services list
        if (checkbox.IsChecked == true)
        {

            CarList_Selected.Cars.Add(car);
        }
        else
        {
            CarList_Selected.Cars.Remove(car);
        }
    }

    public async void gotoSpecifyServices(object sender, EventArgs e) 
	{
        if (CarList_Selected.Cars.Count > 0)
        {
            await Navigation.PushAsync(new ServicesSelection());
        }
        else
        {
            await DisplayAlert("Error", "Please select at least one car to continue", "OK");
        }
	}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
}