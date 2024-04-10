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
            using var resourceStream = await FileSystem.Current.OpenAppPackageFileAsync("application_default_credentials.json");

            if (resourceStream is FileStream)
            {
                string absolutePath = (resourceStream as FileStream).Name;
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", absolutePath);
            }
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
