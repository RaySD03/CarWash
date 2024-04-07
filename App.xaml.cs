namespace CarWash
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
