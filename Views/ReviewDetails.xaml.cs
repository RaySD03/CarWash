namespace CarWash.Views;

public partial class ReviewDetails : ContentPage
{
	public ReviewDetails()
	{
		InitializeComponent();
	}
    public async void confirm(object sender, EventArgs e)
    {
        await DisplayAlert("Note:", "Appointment is scheduled. You can access it from 'my appointments'.", "OK");
        await Navigation.PopToRootAsync();
    }
}