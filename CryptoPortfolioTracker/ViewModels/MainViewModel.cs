using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Helpers;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.User;
using CryptoPortfolioTracker.Views;
using Shared;
using System.Collections.ObjectModel;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        [ObservableProperty]
        private UserInfoDto userInfo;

        public ObservableCollection<PortfolioDto> PortfolioDtos { get; set; } = [];

        public MainViewModel(IAuthService authService,
                                 IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
            UserInfo = new ();
            _ = GetUserInfo();
        }

        [RelayCommand]
        private async Task Logout()
        {
            if (authService.Logout())
                await Shell.Current.GoToAsync(nameof(LoginPage));
            else
                await Shell.Current.DisplayAlert("Fail", "failed to logout", "ok");
        }


        private async Task GetUserInfo()
        {
            try
            {
                UserInfo = await userService.GetUserInformation();
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error during connection {ex.Message}");
            }
        }
    }
}
