using CarWash.Views;

namespace CarWash
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var getUserKey = Preferences.Get("IsLoggedIn", false);

            if (getUserKey == true)
            {
                AgentHomeScreen.IsVisible = false;
                MyAppShell.CurrentItem = HomeScreen;
            }
            else
            {
                MyAppShell.CurrentItem = LoginPage;
            }
        }
       
    }
}
