
using CarWash.Models;
using Google.Cloud.Firestore;
using System.Collections.ObjectModel;

namespace CarWash.Views;

public partial class AppointmentDetails : ContentPage
{
	public AppointmentDetails(string ApptID, string AgentName, string AgentID, string Date, string Time)
	{
		InitializeComponent();

        CarListCollectionView.ItemsSource = CarList_Services.Cars;
        var name = AgentName;
		var splitted = name.Split(' ', 2);

		NameLabel.Text = splitted[0];
		LastNameLabel.Text = splitted[1];
		DateLabel.Text = Date;
		TimeLabel.Text = Time;
        getIncludedCars(ApptID);
    }
    public async void getIncludedCars(string ApptID)
    {
        CarList_Services.Cars.Clear();
        try
        {
            var email = Preferences.Get("UserEmail", "");
            var vm = BindingContext as Car;

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Appointments").Document(ApptID).Collection("Cars").GetSnapshotAsync();

            await Task.Run(async () =>
            {
                int i = 1;
                foreach (DocumentSnapshot doc in await docRef.ConfigureAwait(false))
                {
                    var car = new CarList_Services { Identifier = "", Make = "", Model = "", Year = "", Color = "", Service = "" };
                    DocumentSnapshot retrieved_car = await db.Collection("users").Document(email.ToString()).Collection("Appointments").Document(ApptID).Collection("Cars").Document("Car " + i).GetSnapshotAsync();

                    foreach (KeyValuePair<string, object> pair in retrieved_car.ToDictionary())
                    {
                        if (pair.Key == "Make")
                        {
                            car.Make = (string)pair.Value;
                        }
                        if (pair.Key == "Model")
                        {
                            car.Model = (string)pair.Value;
                        }
                        if (pair.Key == "Year")
                        {
                            car.Year = (string)pair.Value;
                        }
                        if (pair.Key == "Color")
                        {
                            car.Color = (string)pair.Value;
                        }
                        if (pair.Key == "Service")
                        {
                            car.Service = (string)pair.Value;
                        }
                    }
                   
                    CarList_Services.Cars.Add(car);
                    i++;
                }
            });
        }
        catch (Exception ex)
        {

        }
    }
}