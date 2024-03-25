using CarWash.ViewModels;

namespace CarWash.Views;

public partial class UserProfile : ContentPage
{
	public UserProfile()
	{
		InitializeComponent();
		BindingContext = new UserProfileViewModel();
	}
}