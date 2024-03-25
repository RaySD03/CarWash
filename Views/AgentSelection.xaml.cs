namespace CarWash.Views;

public partial class AgentSelection : ContentPage
{
	public AgentSelection()
	{
		InitializeComponent();
	}
	public async void gotoReviewDetails(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ReviewDetails());
	}
}