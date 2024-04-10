using CarWash.Models;
using Google.Cloud.Firestore;

namespace CarWash.Views;

public partial class ReviewDetails : ContentPage
{
	public ReviewDetails()
	{
		InitializeComponent();
        CarListCollectionView.ItemsSource = CarList_Services.Cars;
        AgentLabel.Text = Appointment.MyAppointment.Agent;
        DateLabel.Text = Appointment.MyAppointment.Date;
        TimeLabel.Text = Appointment.MyAppointment.Time;
        AgentIDLabel.Text = Appointment.MyAppointment.AgentID;
    }
    public async void confirm(object sender, EventArgs e)
    {
        try
        {
            var email = Preferences.Get("UserEmail", "");

            // Connect to db and add appointment
            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Appointments").Document();
           
            docRef.CreateAsync(new { Date = Appointment.MyAppointment.Date, Time = Appointment.MyAppointment.Time, Agent = Appointment.MyAppointment.Agent, AgentID = Appointment.MyAppointment.AgentID });

            foreach (var car in CarList_Services.Cars)
            {
                //docRef.CreateAsync(new { Make = car.Make, Model= car.Model, Year = car.Year, Color = car.Color });
            }
            
            await DisplayAlert("Note:", "Appointment is scheduled. You can access it from 'my appointments'.", "OK");
            await Navigation.PopToRootAsync();
        } 
        catch (Exception ex) 
        { 

        }
    }
}