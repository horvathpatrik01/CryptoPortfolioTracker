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
            Current.UserAppTheme = Application.Current.RequestedTheme;
            MainPage = new AppShell(navigationService, authService);
        }
    }
}