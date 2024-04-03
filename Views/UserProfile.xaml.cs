using CarWash.ViewModels;
using Newtonsoft.Json;

namespace CarWash.Views;

public partial class UserProfile : ContentPage
{
	public UserProfile()
	{
		InitializeComponent();
		BindingContext = new UserProfileViewModel();
		GetProfileInfo();
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