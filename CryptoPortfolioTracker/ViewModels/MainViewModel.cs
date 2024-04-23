using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Helpers;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
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
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private UserInfoDto userInfo;

        public ObservableCollection<PortfolioDto> PortfolioDtos { get; set; } = [];

        public MainViewModel(IAuthService authService,
                             IUserService userService,
                             INavigationService navigationService)
        {
            this.authService = authService;
            this.userService = userService;
            this.navigationService = navigationService;
            UserInfo = new();
        }

        [RelayCommand]
        private async Task Logout()
        {
            if (authService.Logout())
                await navigationService.NavigateToAsync($"//{nameof(LoginPage)}");
            else
                await Shell.Current.DisplayAlert("Fail", "failed to logout", "ok");
        }

        [RelayCommand]
        private async Task GetUserInfo()
        {
            try
            {
                UserInfo = await userService.GetUserInformation();
                PortfolioDtos.Add(new PortfolioDto
                {
                    Id = 1,
                    Icon = "dotnet_bot.png",
                    Name = "Name",
                    PortfolioType = PortfolioType.Default,
                    Assets = new List<AssetDto>()
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error during connection {ex.Message}");
            }
        }
    }
}