using CarWash.Models;

namespace CarWash.Views;

public partial class SelectVehicles : ContentPage
{
	public SelectVehicles()
	{
        BindingContext = new Garage();
		InitializeComponent();
        CarList_Selected.Cars.Clear();
        CarListCollectionView.ItemsSource = Garage.Cars;
	}
    public void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = sender as CheckBox;
        var selected = checkbox.BindingContext as Garage;
        var car = new CarList_Selected { Identifier = selected.Identifier, Make = selected.Make, Model = selected.Model, Color = selected.Color, Year = selected.Year, Service = "TBD" };

        // Add or remove checked cars from the collectionview to services list
        if (checkbox.IsChecked == true)
        {
            
            CarList_Selected.Cars.Add(car);
        }
        else
        {
            DisplayAlert("Info", "Delete:" + selected.Model + checkbox.ToString(), "OK");
            CarList_Selected.Cars.Remove(CarList_Selected.Cars.Where(i => i.Identifier == selected.Identifier).Single());
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
}