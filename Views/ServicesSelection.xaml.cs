using CarWash.Models;

namespace CarWash.Views;

public partial class ServicesSelection : ContentPage
{
	public ServicesSelection()
	{
		InitializeComponent();
        CarListCollectionView.ItemsSource = CarList_Services.Cars;
    }
	public async void gotoAgentSelection(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AgentSelection());
	}

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var option = sender as Picker;
        var car = option.BindingContext as CarList_Services;
        
    }
}