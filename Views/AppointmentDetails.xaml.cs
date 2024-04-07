namespace CarWash.Views;

public partial class AppointmentDetails : ContentPage
{
	public AppointmentDetails(string AgentName, string AgentID, string Date, string Time)
	{
		InitializeComponent();

		var name = AgentName;
		var splitted = name.Split(' ', 2);

		NameLabel.Text = splitted[0];
		LastNameLabel.Text = splitted[1];
		DateLabel.Text = Date;
		TimeLabel.Text = Time;
    }
}