using CarWash.Models;
using System.Collections.ObjectModel;


namespace CarWash.Views;

public partial class HomeScreen : ContentPage
{
    public HomeScreen()
	{
		InitializeComponent();
        CarList.Cars.Add(new CarList { Make = "BMW", Model = "Turbo", Year = "2002", Icon = "car_list_icon.png" });
        CarList.Cars.Add(new CarList { Make = "Honda", Model = "Accord", Year = "2018", Icon = "car_list_icon.png" });
        CarList.Cars.Add(new CarList { Make = "Hyundai", Model = "Elantra", Year = "2022", Icon = "car_list_icon.png" });
        CarList.Cars.Add(new CarList { Make = "Volkswagen", Model = "Golf", Year = "2019", Icon = "car_list_icon.png" });
        getCarList();

        GC.Collect();
	}
    private void getCarList()
	{
        CarListCollectionView.ItemsSource = CarList.Cars;          
    }
	public async void goToSchedule(object sender, EventArgs e)
	{
        if (CarList.Cars.Count > 0)
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