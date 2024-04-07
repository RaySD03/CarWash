using CarWash.Models;
using Google.Cloud.Firestore;
using Google.Type;

namespace CarWash.Views;

public partial class AgentSelection : ContentPage
{
	public AgentSelection()
	{
		InitializeComponent();
		AgentListCollectionView.ItemsSource = Agent.Agents;
        getAgentList();
    }
    public async void gotoReviewDetails(object sender, EventArgs e)
    {
        var button = sender as Button;
        var agent = button.BindingContext as Agent;

        Appointment.MyAppointment.Agent = agent.Name + " " + agent.LastName;
        Appointment.MyAppointment.AgentID = agent.Identifier;

        await Navigation.PushAsync(new ReviewDetails());
    }
    public async Task getAgentList()
    {
        Agent.Agents.Clear();

        try
        {
            FirestoreDb db = FirestoreDb.Create("carwash-da88f");
            var docRef = db.Collection("agents").Document("92064").Collection("Local").GetSnapshotAsync();

            await Task.Run(async () =>
            {
                foreach (DocumentSnapshot doc in await docRef.ConfigureAwait(false))
                {
                    var agent = new Agent { Name = "", LastName = ""};
                    DocumentSnapshot retrieved_agent = await db.Collection("agents").Document("92064").Collection("Local").Document(doc.Id).GetSnapshotAsync();

                    agent.Identifier = doc.Id;

                    foreach (KeyValuePair<string, object> pair in retrieved_agent.ToDictionary())
                    {
                        if (pair.Key == "Name")
                        {
                            agent.Name = (string)pair.Value;
                        }
                        if (pair.Key == "LastName")
                        {
                            agent.LastName = (string)pair.Value;
                        }
                        if (pair.Key == "Rating")
                        {
                            agent.Rating = "star_rating_" + (string)pair.Value + ".png";
                        }
                    }
                    // Add agent to agent list
                    Agent.Agents.Add(agent);
                }
                await ListLoadingProgressBar.ProgressTo(1, 500, Easing.Linear);
               
            });
        }
        catch (Exception ex)
        {

        }
        ListLoadingProgressBar.IsVisible = false;
    }
}