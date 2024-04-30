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
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.Views.Popups;

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
        private PortfolioDto selectedPortfolio;

        [ObservableProperty]
        private PortfolioToAddDto portfolioToAdd;

        [ObservableProperty]
        private string newPortfolioName;

        [ObservableProperty]
        private bool isEditing = false;

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
            UserInfo = new();
            PortfolioToAdd = new();
            SelectedPortfolio = new();
            NewPortfolioName = "";
            Portfolios = new ObservableCollection<PortfolioDto>();

            this._authService = authService;
            this._userService = userService;
            this._navigationService = navigationService;
            this._portfolioSevice = portfolioSevice;
            this._exchangeService = exchangeService;
        }

        public async Task IsUserAuthenticated()
        {
            if (await _authService.IsTokenExpired())
            {
                await Logout();
            }

            await GetUserInfo();
            await GetPortfolios();
        }

        public async Task GetUserInfo()
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

        public async Task GetPortfolios()
        {
            try
            {
                IEnumerable<PortfolioDto>? portfolios = await _portfolioSevice.GetPortfolios();
                if (portfolios is not null && portfolios.Any())
                {
                    Portfolios.Clear();
                    portfolios.ToList().ForEach(p =>
                    {
                        if (!Portfolios.Contains(p))
                            Portfolios.Add(p);
                    });
                }
                else if (portfolios is null)
                {
                    await Shell.Current.DisplayAlert("GetPortfoliosError", "The portfolios couldn't be retrieved from the server.", "Ok");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error during connection {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task GetPortfolio(PortfolioDto selectedPortfolioParam)
        {
            if (IsEditing)
            {
                return;
            }
            if (selectedPortfolioParam.Address is null && selectedPortfolioParam.ApiKey is null && selectedPortfolioParam.Assets is null)
            {
                PortfolioDto? portfolio = await _portfolioSevice.GetPortfolio(selectedPortfolioParam.Id);
                // Find the index of the existing SelectedPortfolio in the Portfolios collection
                int index = Portfolios.IndexOf(selectedPortfolioParam);
                // Update the Portfolios collection with the new portfolio data
                if (index != -1 && portfolio is not null && portfolio.Id.Equals(selectedPortfolioParam.Id))
                {
                    Portfolios[index] = portfolio;
                    // Update SelectedPortfolio with the new data
                    SelectedPortfolio = portfolio;
                }
                else
                {
                    await Shell.Current.DisplayAlert("GetPortfolioError", "The portfolios couldn't be retrieved from the server.", "Ok");
                }

                switch (selectedPortfolioParam.PortfolioType)
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
        }

        [RelayCommand]
        public async Task AddPortfolio(PortfolioToAddDto portfolioToAdd)
        {
            PortfolioDto? newlyAddedPortfolio = await _portfolioSevice.AddPortfolio(portfolioToAdd);
            if (newlyAddedPortfolio is not null)
            {
                Portfolios.Add(newlyAddedPortfolio);
            }
            else
            {
                await Shell.Current.DisplayAlert("AddPortfolioError", "The portfolio couldn't be added to the server.", "Ok");
            }
        }

        public async Task EditPortfolio(int portfolioId, string newPortfolioName, string newPortfolioIcon)
        {
            if (!IsEditing)
                return;

            PortfolioDto? editedPortfolio = await _portfolioSevice.ChangePortfolioName(portfolioId, newPortfolioName);
            if (editedPortfolio is not null)
            {
                var p = Portfolios.FirstOrDefault(p => p.Id == portfolioId);
                if (p != null)
                {
                    p.Name = editedPortfolio.Name;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("EditError", "The portfolio couldn't be removed from the server!", "Ok");
            }
        }

        [RelayCommand]
        public async Task RemovePortfolioAsync(PortfolioDto selectedPortfolioParam)
        {
            if (!IsEditing)
                return;

            PortfolioDto? deletedPortfolio = await _portfolioSevice.RemovePortfolio(selectedPortfolioParam.Id);
            if (deletedPortfolio is not null && deletedPortfolio.Id.Equals(selectedPortfolioParam.Id))
            {
                var portfolioToRemove = Portfolios.FirstOrDefault(p => p.Id == selectedPortfolioParam.Id);

                if (portfolioToRemove != null)
                {
                    Portfolios.Remove(portfolioToRemove);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("RemoveError", "The portfolio couldn't be removed from the server!", "Ok");
            }
        }

        [RelayCommand]
        private void ChangeTheme()
        {
            Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
        }

        [RelayCommand]
        public void CreatePortfolio()
        {
            Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioPopup(new CreatePortfolioViewModel(_navigationService, this)));
        }

        public void StartEditPortfolioPopup()
        {
            Shell.Current.CurrentPage.ShowPopup(new EditPortfolioPopup(new EditPortfolioViewModel(_navigationService, this)));
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
        private void ToggleEdit()
        {
            IsEditing = !IsEditing;
        }
    }
}