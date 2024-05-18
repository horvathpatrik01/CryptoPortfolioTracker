using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.ViewModels.Base;
using CryptoPortfolioTracker.Views;
using Shared.Auth;
using System.Diagnostics;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService authService;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private LoginModel _loginModel;

        public LoginViewModel(IAuthService authService,
                              INavigationService navigationService)
            : base(navigationService)
        {
            this.authService = authService;
            this.navigationService = navigationService;
            LoginModel = new();
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                var loginResult = new LoginResult
                {
                    Token = null,
                    Error = null,
                    Successful = false
                };
                await IsBusyFor(async () => loginResult = await authService.Login(LoginModel));

                if (loginResult != null && loginResult.Successful)
                {
                    await navigationService.NavigateToAsync($"{nameof(PortfolioPage)}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        private async Task GoToRegisterPage()
        {
            await navigationService.NavigateToAsync(nameof(RegisterPage));
        }
    }
}