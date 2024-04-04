using CarWash.Models;
using System.Collections.ObjectModel;


namespace CarWash.Views;
public partial class HomeScreen : ContentPage
{
    public HomeScreen()
	{
		InitializeComponent();
        Garage.Cars.Add(new Garage { Make = "BMW", Model = "Turbo", Year = "2002", Color = "Orange", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Make = "Honda", Model = "Accord", Year = "2018", Color = "White", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Make = "Hyundai", Model = "Elantra", Year = "2022", Color = "Blue", Icon = "car_list_icon.png" });
        Garage.Cars.Add(new Garage { Make = "Volkswagen", Model = "Golf", Year = "2019", Color = "Silver", Icon = "car_list_icon.png" });
        getCarList();
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