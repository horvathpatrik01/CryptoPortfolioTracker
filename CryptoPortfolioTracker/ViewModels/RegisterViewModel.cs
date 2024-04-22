using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Views;
using Shared.Auth;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService authService;
        [ObservableProperty]
        private RegisterModel _registerModel;

        public RegisterViewModel(IAuthService authService)
        {
            this.authService = authService;
            RegisterModel = new();
        }

        [RelayCommand]
        private async Task Register()
        {
            var registerResult = await authService.Register(RegisterModel);
            if (registerResult != null && registerResult.Successful)
            {
                await Shell.Current.DisplayAlert("Hurray", "You are successfully registered in the DB", "Ok");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
