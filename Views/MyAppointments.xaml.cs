namespace CarWash.Views;

using CarWash.Models;
using Google.Cloud.Firestore;
public partial class MyAppointments : ContentPage
{
	public MyAppointments()
	{    
        getAppointments();

        InitializeComponent();
		
        AppointmentsCollectionView.ItemsSource = Appointment.MyAppointments;
    }
    public async Task getAppointments()
    {
        try
        {
            var email = Preferences.Get("UserEmail", "");

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Appointments").GetSnapshotAsync();

            await Task.Run(async () =>
            {
                foreach (DocumentSnapshot doc in await docRef.ConfigureAwait(false))
                {
                    var appointment = new Appointment { AgentID = "", Agent = "", Date = "", Time = "" };
                    DocumentSnapshot retrieved_appointment = await db.Collection("users").Document(email.ToString()).Collection("Appointments").Document(doc.Id).GetSnapshotAsync();

                    foreach (KeyValuePair<string, object> pair in retrieved_appointment.ToDictionary())
                    {
                        if (pair.Key == "AgentID")
                        {
                            appointment.AgentID = (string)pair.Value;
                        }
                        if (pair.Key == "Agent")
                        {
                            appointment.Agent = (string)pair.Value;
                        }
                        if (pair.Key == "Date")
                        {
                            appointment.Date = (string)pair.Value;
                        }
                        if (pair.Key == "Time")
                        {
                            appointment.Time = (string)pair.Value;
                        }
                    }

                    Appointment.MyAppointments.Add(appointment);
                }
            });

            if (Appointment.MyAppointments.Count == 0)
            {
                NoAppointmentLabel.IsVisible = true;
            }
            else
            {
                NoAppointmentLabel.IsVisible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}