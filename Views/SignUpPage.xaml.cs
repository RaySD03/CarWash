using CarWash.ViewModels;
namespace CarWash.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
    }
    private void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        // This ensures the event handler does not react unless the change is to checked state.
        if (!e.Value) return;

        if (sender == userCheckBox)
        {
            agentCheckBox.IsChecked = false;
        }
        else if (sender == agentCheckBox)
        {
            userCheckBox.IsChecked = false;
        }
    }

}