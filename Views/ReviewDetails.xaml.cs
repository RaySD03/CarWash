using CarWash.Models;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;

namespace CarWash.Views;

public partial class ReviewDetails : ContentPage
{
    public static Dictionary<string, object> IncludedCars = new Dictionary<string, object> { };
    public ReviewDetails()
	{
		InitializeComponent();
        CarListCollectionView.ItemsSource = CarList_Services.Cars;
        AgentLabel.Text = Appointment.MyAppointment.Agent;
        DateLabel.Text = Appointment.MyAppointment.Date;
        TimeLabel.Text = Appointment.MyAppointment.Time;
        AgentIDLabel.Text = Appointment.MyAppointment.AgentID;
    }
    public async void confirm_appointment(object sender, EventArgs e)
    {
        try
        {
            var email = Preferences.Get("UserEmail", "");

            // Connect to db and add appointment
            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Appointments").Document();

            Dictionary<string, object> ApptDetails = new Dictionary<string, object>
            {
                { "ApptID" , (string)docRef.Id},
                { "Date" , (string)Appointment.MyAppointment.Date },
                {  "Time" , (string)Appointment.MyAppointment.Time },
                { "Agent" , (string)Appointment.MyAppointment.Agent },
                { "AgentID" , (string)Appointment.MyAppointment.AgentID }
            };

            var ApptID = docRef.Id;
            await docRef.SetAsync(ApptDetails);

            int i = 1;
            foreach (var car in CarList_Services.Cars)
            {
                var docRef1 = db.Collection("users").Document(email.ToString()).Collection("Appointments").Document(ApptID).Collection("Cars").Document("Car " + i);
                IncludedCars.Add("Make", car.Make);
                IncludedCars.Add("Model", car.Model);
                IncludedCars.Add("Year", car.Year);
                IncludedCars.Add("Color", car.Color);
                IncludedCars.Add("Service", car.Service);

                docRef1.CreateAsync(IncludedCars);

                IncludedCars.Clear();
                i++;
            }       
            
            await DisplayAlert("Note:", "Appointment is scheduled. You can access it from 'my appointments'.", "OK");
            await Navigation.PopToRootAsync();
        } 
        catch (Exception ex) 
        { 

        }
    }
}