using CarWash.ViewModels;
using Newtonsoft.Json;

namespace CarWash.Views;

public partial class UserProfile : ContentPage
{
	public UserProfile()
	{
		InitializeComponent();
		BindingContext = new UserProfileViewModel();	
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        GetProfileInfo();
    }
    public async void goToAddressForm(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyAddress());
    }
    private void GetProfileInfo()
	{
		var email = Preferences.Get("UserEmail", "");
		if (email != null)
		{
			Email.Text = email;
		}	
    }
}