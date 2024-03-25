using CarWash.Models;

namespace CarWash.Views;

public partial class SelectVehicles : ContentPage
{
	public SelectVehicles()
	{
		InitializeComponent();
        CarList_Services.Cars.Clear();
        CarListCollectionView.ItemsSource = CarList.Cars;
	}
    public void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        var checkbox = sender as CheckBox;
        var car = checkbox.BindingContext as CarList;

        // Add or remove checked cars from the collectionview to services list
        if (checkbox.IsChecked == true)
        {
            CarList_Services.Cars.Add(car);
        }
        else
        {
            CarList_Services.Cars.Remove(car);
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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
}