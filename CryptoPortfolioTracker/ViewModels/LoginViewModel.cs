using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Views;
using Shared.Auth;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService authService;
        [ObservableProperty]
        private LoginModel _loginModel;

        public LoginViewModel(IAuthService authService)
        {
            this.authService = authService;
            LoginModel = new();
        }

        [RelayCommand]
        private async Task Login()
        {
            var loginResult=await authService.Login(LoginModel);
            if(loginResult != null && loginResult.Successful)
            {
                await Shell.Current.DisplayAlert("Hurray", "you have been logged in", "Ok");
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
        }

        [RelayCommand]
        private async Task GoToRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
