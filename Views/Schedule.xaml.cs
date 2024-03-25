namespace CarWash.Views;

public partial class Schedule : ContentPage
{
	public Schedule()
	{
		InitializeComponent();
	}

	public async void goToSelectVehicles(object sender, EventArgs e) 
	{
		await Navigation.PushAsync(new SelectVehicles());
	}
}