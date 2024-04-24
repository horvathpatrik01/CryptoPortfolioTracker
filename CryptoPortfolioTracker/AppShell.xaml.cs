using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Views;
using Microsoft.Maui.Hosting;

namespace CryptoPortfolioTracker
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthService _authService;

        public AppShell(INavigationService navigationService, IAuthService authService)
        {
            this._navigationService = navigationService;
            this._authService = authService;
            AppShell.InitializeRouting();
            InitializeComponent();
        }

        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            if (Handler is not null)
            {
                if (await _authService.IsTokenExpired())
                    _authService.Logout();

                await _navigationService.InitializeAsync();

            }
        }

        private static void InitializeRouting()
        {
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(PortfolioPage), typeof(PortfolioPage));
        }
    }
}