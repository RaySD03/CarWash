using CarWash.Models;
using System.Security.Cryptography;

namespace CarWash.Views;

public partial class Schedule : ContentPage
{
	public Schedule()
	{
		InitializeComponent();
    }

	public async void goToSelectVehicles(object sender, EventArgs e) 
	{
		Appointment.MyAppointment.Date = DatePicker.Date.ToShortDateString();
        Appointment.MyAppointment.Time = TimePicker1.SelectedItem.ToString() + " " + TimePicker2.SelectedItem.ToString();
        await Navigation.PushAsync(new SelectVehicles());
	}
    private void Handle_Time_Change(object sender, EventArgs e)
    {
        var time = sender as Picker;

        if (int.Parse(time.SelectedItem.ToString().Substring(0, 2)) >= 12)
        {
            TimePicker2.SelectedItem = 1;
        }
        else 
        {
            TimePicker2.SelectedItem = 0;
        }
    }
}