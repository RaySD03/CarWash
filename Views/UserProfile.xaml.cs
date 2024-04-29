using CarWash.ViewModels;
using Firebase.Auth;
using Newtonsoft.Json;
using static Google.Rpc.Context.AttributeContext.Types;

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
    private async void GetProfileInfo()
	{
		var email = Preferences.Get("UserEmail", "");
        var fullname = Preferences.Get("Fullname", "");
        if (email != null)
		{
			Email.Text = email;
            Fullname.Text = fullname;
		}
    }
}