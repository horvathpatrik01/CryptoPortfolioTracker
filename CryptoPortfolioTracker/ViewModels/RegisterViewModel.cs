using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Views;
using Shared.Auth;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService authService;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private RegisterModel _registerModel;

        public RegisterViewModel(IAuthService authService,
            INavigationService navigationService)
        {
            this.authService = authService;
            this.navigationService = navigationService;
            RegisterModel = new();
        }

        [RelayCommand]
        private async Task Register()
        {
            var registerResult = await authService.Register(RegisterModel);
            if (registerResult != null && registerResult.Successful)
            {
                await Shell.Current.DisplayAlert("Hurray", "You are successfully registered in the DB", "Ok");
                await navigationService.PopAsync();
            }
        }

        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await navigationService.PopAsync();
        }
    }
}