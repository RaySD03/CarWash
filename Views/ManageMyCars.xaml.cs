using CarWash.Models;

namespace CarWash.Views;

public partial class ManageMyCars : ContentPage
{
	public ManageMyCars()
	{
		InitializeComponent();
        BindingContext = new CarList();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        getCarList();
    }
    private void getCarList()
    {
        CarListCollectionView.ItemsSource = CarList.Cars;

        if (CarList.Cars.Count == 0)
        {
            Label.Text = "Tap the + button to add a car.";
            CarListCollectionView.IsVisible = false;
            addBtn.IsVisible = true;
        }
        else
        {
            Label.Text = CarList.Cars.Count + " / 5 cars";
            CarListCollectionView.IsVisible = true;

            if (CarList.Cars.Count == 5)
                addBtn.IsVisible = false;
        }
    }
    public void Remove_Selected_Car(object sender, EventArgs e)
    {
        var button = sender as Button;
        var car = button.BindingContext as CarList;
        var vm = BindingContext as CarList;
        vm.RemoveCommand.Execute(car);
        Label.Text = CarList.Cars.Count + " / 5 cars";

        if (CarList.Cars.Count < 5) 
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