using CarWash.Models;
using Google.Cloud.Firestore;

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
                    }
                    // Add agent
                    Agent.Agents.Add(agent);
                }
            });
        }
        catch (Exception ex)
        {

        }
    }
}