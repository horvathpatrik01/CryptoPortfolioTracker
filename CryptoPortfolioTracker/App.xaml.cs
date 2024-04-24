using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;

namespace CryptoPortfolioTracker
{
    public partial class App : Application
    {
        public App(INavigationService navigationService,
            IAuthService authService)
        {

            InitializeComponent();

            // Start in light mode
            UserAppTheme = AppTheme.Light;

            MainPage = new AppShell(navigationService, authService);
        }

    }
}