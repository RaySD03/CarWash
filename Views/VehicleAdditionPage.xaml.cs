using CarWash.Models;

namespace CarWash.Views;

public partial class VehicleAdditionPage : ContentPage
{
	public VehicleAdditionPage()
	{
		InitializeComponent();
	}
    public void addCar(object sender, EventArgs e)
    {
        if (CarList.Cars.Count < 5) 
        {
            CarList.Cars.Add(new CarList { Make = makeEntry.Text, Model = modelEntry.Text, Year = yearEntry.Text, Icon = "car_list_icon.png" });
            Console.WriteLine("Car added successfully");

            this.Navigation.PopAsync();
        }
        else
        {
           
        }     
    }
}