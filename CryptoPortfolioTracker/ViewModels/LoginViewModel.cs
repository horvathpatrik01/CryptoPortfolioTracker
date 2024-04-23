using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Views;
using Shared.Auth;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService authService;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private LoginModel _loginModel;

        public LoginViewModel(IAuthService authService,
            INavigationService navigationService)
        {
            this.authService = authService;
            this.navigationService = navigationService;
            LoginModel = new();
        }

        [RelayCommand]
        private async Task Login()
        {
            var loginResult = await authService.Login(LoginModel);
            if (loginResult != null && loginResult.Successful)
            {
                await navigationService.NavigateToAsync($"{nameof(MainPage)}");
            }
        }

        [RelayCommand]
        private async Task GoToRegisterPage()
        {
            await navigationService.NavigateToAsync(nameof(RegisterPage));
        }
    }
}