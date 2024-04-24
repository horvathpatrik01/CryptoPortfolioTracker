using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Views;
using Shared.Auth;
using System.Collections.ObjectModel;
using Shared;
using CryptoPortfolioTracker.Services.User;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Services.Portfolio;
using System.Diagnostics;
using CryptoPortfolioTracker.Services.Exchange;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class PortfolioViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IPortfolioSevice _portfolioSevice;
        private readonly IExchangeService _exchangeService;

        [ObservableProperty]
        private UserInfoDto userInfo;

        [ObservableProperty]
        private PortfolioToAddDto portfolioToAdd;

        [ObservableProperty]
        private string newPortfolioName;

        [ObservableProperty]
        private PortfolioDto selectedPortfolio;

        public ObservableCollection<string> Errors { get; set; } = [];

        public ObservableCollection<PortfolioDto> Portfolios { get; set; }
        public ObservableCollection<AssetDto> AssetItemSource { get; set; } = [];

        public PortfolioViewModel(
            IAuthService authService,
            IUserService userService,
            INavigationService navigationService,
            IPortfolioSevice portfolioSevice,
            IExchangeService exchangeService)
        {
            Portfolios = new ObservableCollection<PortfolioDto>();

            // Mock data
            Portfolios = new ObservableCollection<PortfolioDto>
            {
                new PortfolioDto { Id = 0 , Name = "Portfolio 1", Icon="user.png", PortfolioType=PortfolioType.Default},
                new PortfolioDto { Id = 1 , Name = "Portfolio 2", Icon = "user.png", PortfolioType = PortfolioType.Default },
                new PortfolioDto { Id = 2 , Name = "Portfolio 3", Icon="user.png", PortfolioType=PortfolioType.Default},
                new PortfolioDto { Id = 3 , Name = "Portfolio 4", Icon = "user.png", PortfolioType = PortfolioType.Default },
                new PortfolioDto { Id = 4 , Name = "Portfolio 5", Icon = "user.png", PortfolioType = PortfolioType.Default }
            };
            this._authService = authService;
            this._userService = userService;
            this._navigationService = navigationService;
            this._portfolioSevice = portfolioSevice;
            this._exchangeService = exchangeService;
        }

        [RelayCommand]
        public async Task GetPortfolios()
        {
            try
            {
                IEnumerable<PortfolioDto>? portfolios = await _portfolioSevice.GetPortfolios();
                if (portfolios is not null && portfolios.Any())
                {
                    portfolios.ToList().ForEach(p =>
                    {
                        if (!Portfolios.Contains(p))
                            Portfolios.Add(p);
                    });
                }
                else
                {
                    await Shell.Current.DisplayAlert("EditError", "The portfolios couldn't be retrieved from the server.", "Ok");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error during connection {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task GetPortfolio()
        {
            if (SelectedPortfolio.Address is null && SelectedPortfolio.ApiKey is null && SelectedPortfolio.Assets is null)
            {
                PortfolioDto? portfolio = await _portfolioSevice.GetPortfolio(SelectedPortfolio.Id);
                // Find the index of the existing SelectedPortfolio in the Portfolios collection
                int index = Portfolios.IndexOf(SelectedPortfolio);

                // Update the Portfolios collection with the new portfolio data
                if (index != -1 && portfolio is not null)
                {
                    Portfolios[index] = portfolio;
                    // Update SelectedPortfolio with the new data
                    SelectedPortfolio = portfolio;
                }
                switch (SelectedPortfolio.PortfolioType)
                {
                    case PortfolioType.Default:
                        // Get Asset Prices
                        break;

                    case PortfolioType.CexAccount:
                        // Get Profile from exchange
                        break;

                    case PortfolioType.Wallet:
                        // Get transactions and assets from
                        break;

                    default: break;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("EditError", "The portfolios couldn't be retrieved from the server.", "Ok");
            }
        }

        [RelayCommand]
        public async Task AddPortfolio()
        {
            PortfolioDto? newlyAddedPortfolio = await _portfolioSevice.AddPortfolio(PortfolioToAdd);
            if (newlyAddedPortfolio is not null)
            {
                Portfolios.Add(newlyAddedPortfolio);
            }
            else
            {
                await Shell.Current.DisplayAlert("EditError", "The portfolio couldn't be added to the server.", "Ok");
            }
        }

        [RelayCommand]
        public async Task EditPortfolio(int portfolioId)
        {
            PortfolioDto? editedPortfolio = await _portfolioSevice.ChangePortfolioName(portfolioId, NewPortfolioName);
            if (editedPortfolio is not null)
            {
                var p = Portfolios.FirstOrDefault(p => p.Id == portfolioId);
                if (p != null)
                    p.Name = editedPortfolio.Name;
            }
            else
            {
                await Shell.Current.DisplayAlert("EditError", "The portfolio couldn't be removed from the server!", "Ok");
            }
        }

        [RelayCommand]
        public async Task RemovePortfolioAsync(int portfolioId)
        {
            PortfolioDto? deletedPortfolio = await _portfolioSevice.RemovePortfolio(portfolioId);
            if (deletedPortfolio is not null && deletedPortfolio.Id.Equals(portfolioId))
            {
                var portfolioToRemove = Portfolios.FirstOrDefault(p => p.Id == portfolioId);

                if (portfolioToRemove != null)
                {
                    Portfolios.Remove(portfolioToRemove);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("EditError", "The portfolio couldn't be removed from the server!", "Ok");
            }
        }

        [RelayCommand]
        private void ChangeTheme()
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
        }

        [RelayCommand]
        private async Task Logout()
        {
            if (_authService.Logout())
                await _navigationService.NavigateToAsync($"//{nameof(LoginPage)}");
            else
                await Shell.Current.DisplayAlert("Fail", "failed to logout", "ok");
        }

        [RelayCommand]
        private async Task GetUserInfo()
        {
            try
            {
                UserInfo = await _userService.GetUserInformation();
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error during connection {ex.Message}");
            }
        }
    }
}