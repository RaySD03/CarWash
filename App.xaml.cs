using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace CarWash
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //initFirestore();
            MainPage = new AppShell();
        }
        public async Task<FirestoreDb> initFirestore()
        {
            try
            {  
                var stream = await FileSystem.OpenAppPackageFileAsync("application_default_credentials.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                FirestoreClientBuilder fbc = new FirestoreClientBuilder { JsonCredentials = contents };
                return FirestoreDb.Create("carwash-da88f", fbc.Build());
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void loadAssets()
        {
            var localPath = Path.Combine(FileSystem.CacheDirectory, "application_default_credentials.json");

            using var json = await FileSystem.OpenAppPackageFileAsync("application_default_credentials.json");
            using var dest = File.Create(localPath);
            await json.CopyToAsync(dest);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);

            dest.Close();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int Width = 620;
            const int Height = 650;

            window.Width = Width;
            window.Height = Height;

            return window;
        }
    }
}
