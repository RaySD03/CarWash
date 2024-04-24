namespace CarWash.Views;

using CarWash.Models;
using Google.Cloud.Firestore;
public partial class MyAppointments : ContentPage
{
	public MyAppointments()
	{    
        InitializeComponent();       
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AppointmentsCollectionView.ItemsSource = Appointment.MyAppointments;
        getAppointments();
        if (Appointment.MyAppointments.Count == 0)
        {
            NoAppointmentLabel.IsVisible = true;
        }
        else
        {
            NoAppointmentLabel.IsVisible = false;
        }
    }
    public async void gotoViewDetails(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var appointment = button.BindingContext as Appointment;

        await Navigation.PushAsync(new AppointmentDetails(appointment.ApptID, appointment.Agent, appointment.AgentID, appointment.Date, appointment.Time));
    }
    public async Task getAppointments()
    {
        Appointment.MyAppointments.Clear();

        try
        {
            var email = Preferences.Get("UserEmail", "");

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Appointments").GetSnapshotAsync();

            await Task.Run(async () =>
            {
                foreach (DocumentSnapshot doc in await docRef.ConfigureAwait(false))
                {
                    var appointment = new Appointment { ApptID ="", AgentID = "", Agent = "", Date = "", Time = "" };
                    DocumentSnapshot retrieved_appointment = await db.Collection("users").Document(email.ToString()).Collection("Appointments").Document(doc.Id).GetSnapshotAsync();

                    foreach (KeyValuePair<string, object> pair in retrieved_appointment.ToDictionary())
                    {
                        if (pair.Key == "ApptID")
                        {
                            appointment.ApptID = (string)pair.Value;
                        }
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
        }
        catch (Exception ex)
        {


        }

    }
}