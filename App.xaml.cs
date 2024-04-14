using Microsoft.Extensions.Configuration;

namespace CarWash
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            loadAssets();
            MainPage = new AppShell();
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
