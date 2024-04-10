using CarWash.Models;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Handlers;
using System.ComponentModel;
using System.Reflection;

namespace CarWash.Views;

public partial class ServicesSelection : ContentPage
{
    public ServicesSelection()
	{
		InitializeComponent();
        CarListCollectionView1.ItemsSource = CarList_Services.Cars;
    }
    public async void gotoAgentSelection(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AgentSelection());
	}
    private void Service_Specified(object sender, EventArgs e)
    {
        var option = sender as Picker;    
        var selected = option.BindingContext as CarList_Services;
        selected.Service = option.SelectedItem.ToString();

        //DisplayAlert("Info", "Identifier:" + selected.Identifier + "\nMake:" + selected.Make +  "\nModel:" + selected.Model, "OK");
    }
}