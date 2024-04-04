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
        if (Garage.Cars.Count < 5) 
        {
            Garage.Cars.Add(new Garage { Make = makeEntry.Text, Model = modelEntry.Text, Year = yearEntry.Text, Color = ColorPicker.SelectedItem.ToString(), Icon = "car_list_icon.png" });
            Console.WriteLine("Car added successfully");

            this.Navigation.PopAsync();
        }
        else
        {
           
        }     
    }
}