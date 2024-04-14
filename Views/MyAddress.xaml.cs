using Google.Cloud.Firestore;

namespace CarWash.Views;

public partial class MyAddress : ContentPage
{
	public MyAddress()
	{
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var email = Preferences.Get("UserEmail", "");

        FirestoreDb db = FirestoreDb.Create("carwash-da88f");
        DocumentSnapshot retrieved_address = await db.Collection("users").Document(email.ToString()).Collection("Address").Document("info").GetSnapshotAsync();

        if (retrieved_address.Exists)
        {
            foreach (KeyValuePair<string, object> pair in retrieved_address.ToDictionary())
            {
                if (pair.Key == "Street")
                {
                    streetEntry.Text = (string)pair.Value;
                }
                if (pair.Key == "City")
                {
                    cityEntry.Text = (string)pair.Value;
                }
                if (pair.Key == "State")
                {
                    stateEntry.Text = (string)pair.Value;
                }
                if (pair.Key == "ZipCode")
                {
                    zipcodeEntry.Text = (string)pair.Value;
                }
            }
        }
    }
    public async void saveAddress(object sender, EventArgs e)
    {
        try
        {
            Dictionary<string, object> Address = new Dictionary<string, object>
            {
                { "Street" , streetEntry.Text},
                { "City" , cityEntry.Text},
                {  "State" , stateEntry.Text},
                { "ZipCode" , zipcodeEntry.Text}
            };

            var email = Preferences.Get("UserEmail", "");

            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            DocumentSnapshot retrieved_address = await db.Collection("users").Document(email.ToString()).Collection("Address").Document("info").GetSnapshotAsync();
            var docRef = db.Collection("users").Document(email.ToString()).Collection("Address").Document("info");
            
            if (retrieved_address.Exists)
            {
                await docRef.UpdateAsync(Address);
            }
            else
            {
                await docRef.CreateAsync(Address);
            }
          
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {

        }    
    }
}