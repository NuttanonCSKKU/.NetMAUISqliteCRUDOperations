namespace GG
{
    public partial class App : Application
    {
        private readonly MainPage _mainPage;

        public App(MainPage mainPage)
        {
            InitializeComponent();
            _mainPage = mainPage; // Store the reference to the MainPage instance
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Set the root page of the application to MainPage
            return new Window(_mainPage);
        }
    }
}
