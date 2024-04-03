using CarWash.ViewModels;
namespace CarWash.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }
}