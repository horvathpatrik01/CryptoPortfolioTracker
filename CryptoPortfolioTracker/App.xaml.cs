using CryptoPortfolioTracker.Services.Navigation;

namespace CryptoPortfolioTracker
{
    public partial class App : Application
    {

        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new AppShell(navigationService);
        }
    }
}
