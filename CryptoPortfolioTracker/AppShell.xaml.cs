using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Views;

namespace CryptoPortfolioTracker
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            AppShell.InitializeRouting();
            InitializeComponent();
        }

        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            if(Handler is not null)
            {
                await _navigationService.InitializeAsync();
            }
        }

        private static void InitializeRouting()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }
    }
}
