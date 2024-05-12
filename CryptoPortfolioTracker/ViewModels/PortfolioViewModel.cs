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
using CryptoPortfolioTracker.Services.Transaction;
using CryptoPortfolioTracker.Models;
using System.Linq;
using System;
using CryptoPortfolioTracker.ViewModels.Base;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class PortfolioViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly ITransactionService _transactionService;
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
        private AssetDto selectedAsset;

        [ObservableProperty]
        private string newPortfolioName;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowAssets))]
        private bool showTransactions;

        public bool ShowAssets => !ShowTransactions;
        public ObservableCollection<string> Errors { get; set; } = [];

        public ObservableCollection<PortfolioDto> Portfolios { get; set; }
        public ObservableCollection<Asset> AssetItemSource { get; set; }
        public ObservableCollection<TransactionDto> TransactionsSource { get; set; }
        public List<Coin> SupportedCoins { get; set; }

        public PortfolioViewModel(
            IAuthService authService,
            ITransactionService transactionService,
            IUserService userService,
            INavigationService navigationService,
            IPortfolioSevice portfolioSevice,
            IExchangeService exchangeService) : base(navigationService)
        {
            UserInfo = new();
            PortfolioToAdd = new();
            SelectedPortfolio = new();
            SelectedAsset = new();
            NewPortfolioName = "";
            AssetItemSource = [];
            Portfolios = [];
            SupportedCoins = [];
            TransactionsSource = [];
            ShowTransactions = false;

            this._authService = authService;
            this._transactionService = transactionService;
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
            await GetAssets();
            if (Portfolios.Any())
                SelectedPortfolio = Portfolios[0];
        }

        public override async Task InitializeAsync()
        {
            if (await _authService.IsTokenExpired())
            {
                await Logout();
            }
            await IsBusyFor(async () =>
            {
                await GetUserInfo();
                await GetPortfolios();
                await GetAssets();
            });
            if (Portfolios.Any())
                SelectedPortfolio = Portfolios[0];
        }

        public async Task GetAssets()
        {
            SupportedCoins = await _transactionService.GetSupportedCoins();
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
            if (IsEditing || selectedPortfolioParam is null)
            {
                return;
            }
            await IsBusyFor(async () =>
            {
                AssetItemSource.Clear();
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
                        switch (selectedPortfolioParam.PortfolioType)
                        {
                            case PortfolioType.Default:
                                // Get Asset Prices

                                await RefreshCoinPrices(portfolio.Assets);
                                PopulateAssetItemSource(portfolio.Assets);
                                break;

                            case PortfolioType.CexAccount:
                                // Get assets from exchange
                                break;

                            case PortfolioType.Wallet:
                                // Get transactions and assets from
                                break;

                            default: break;
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("GetPortfolioError", "The portfolio couldn't be retrieved from the server.", "Ok");
                    }
                }
                else
                {
                    switch (selectedPortfolioParam.PortfolioType)
                    {
                        case PortfolioType.Default:
                            // Get Asset Prices
                            await RefreshCoinPrices(SelectedPortfolio.Assets);
                            PopulateAssetItemSource(SelectedPortfolio.Assets);

                            break;

                        case PortfolioType.CexAccount:
                            // Get assets from exchange
                            break;

                        case PortfolioType.Wallet:
                            // Get transactions and assets from
                            break;

                        default: break;
                    }
                }
            });
            
            ShowTransactions = false;
        }

        private async Task RefreshCoinPrices(List<AssetDto>? assets)
        {
            List<int> coinIds = [];
            if (SupportedCoins.Count == 0) return;
            assets?.ForEach(asset => coinIds.Add(SupportedCoins.Find(a => a.Symbol == asset.Symbol)?.Id ?? -1));
            if (coinIds.Count == 0) return;
            var modifiedCoins = await _transactionService.GetCoinPrices(coinIds);
            modifiedCoins?.ForEach(mc =>
            {
                var coin = SupportedCoins.Find(sc => sc.Id == mc.Id);
                if (coin != null)
                {
                    coin.Quote = mc.Quote;
                }
            });
        }

        public void PopulateAssetItemSource(List<AssetDto>? assets)
        {
            if (assets is null) return;
            foreach (var asset in assets.Where(asset => AssetItemSource.FirstOrDefault(a => a.Id == asset.Id) is null))
            {
                Asset dummyAsset = new Asset
                {
                    Id = asset.Id,
                    IconUrl = asset.IconUrl,
                    Amount = asset.Amount,
                    Name = asset.Name,
                    AvrgBuyPrice = asset.AvrgBuyPrice,
                    Symbol = asset.Symbol,
                    Transactions = asset.Transactions
                };
                Coin? matchedCoinForAsset = SupportedCoins.Find(a => a.Symbol == asset.Symbol);
                SymbolQuote? coinPriceInfo = matchedCoinForAsset?.Quote["USD"];
                dummyAsset.Price = coinPriceInfo?.Price ?? 0m;
                dummyAsset.Percent_Change_1h = coinPriceInfo?.Percent_Change_1h ?? 0f;
                dummyAsset.Percent_Change_24h = coinPriceInfo?.Percent_Change_24h ?? 0f;
                dummyAsset.Percent_Change_7d = coinPriceInfo?.Percent_Change_7d ?? 0f;
                AssetItemSource.Add(dummyAsset);
            }
        }

        [RelayCommand]
        public async Task AddPortfolio(PortfolioToAddDto portfolioToAdd)
        {
            PortfolioDto? newlyAddedPortfolio = await _portfolioSevice.AddPortfolio(portfolioToAdd);
            if (newlyAddedPortfolio is not null)
            {
                Portfolios.Add(newlyAddedPortfolio);
                ShowTransactions = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("AddPortfolioError", "The portfolio couldn't be added to the server.", "Ok");
            }
        }

        public async Task EditPortfolio(PortfolioToEditDto portfolioToEdit)
        {
            if (!IsEditing)
                return;

            PortfolioDto? editedPortfolio = await _portfolioSevice.EditPortfolio(portfolioToEdit);
            if (editedPortfolio is not null)
            {
                var p = Portfolios.FirstOrDefault(p => p.Id == portfolioToEdit.Id);
                if (p != null)
                {
                    p.Name = editedPortfolio.Name;
                    p.Icon = editedPortfolio.Icon;
                    p.IconColor = editedPortfolio.IconColor;
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
        private void ShowTransactionsForAsset(Asset asset)
        {
            ShowTransactions = true;
            SelectedAsset.Name = asset.Name;
            SelectedAsset.AvrgBuyPrice = asset.AvrgBuyPrice;
            SelectedAsset.Symbol = asset.Symbol;
            SelectedAsset.Amount = asset.Amount;
            SelectedAsset.Id = asset.Id;
            SelectedAsset.IconUrl = asset.IconUrl;

            TransactionsSource.Clear();
            foreach (TransactionDto transaction in asset.Transactions)
            {
                if (!TransactionsSource.Contains(transaction))
                    TransactionsSource.Add(transaction);
            }
        }

        [RelayCommand]
        private async Task DeleteAsset(int assetId)
        {
            try
            {
                var deletedAsset = await _transactionService.DeleteAsset(assetId);
                var localAsset = AssetItemSource.FirstOrDefault(a => a.Id.Equals(deletedAsset?.Id));
                var portfolioAsset = Portfolios.FirstOrDefault(p => p.Id.Equals(SelectedPortfolio.Id))?.Assets?.Find(a => a.Id.Equals(deletedAsset?.Id));
                if (localAsset is not null && portfolioAsset is not null)
                {
                    AssetItemSource.Remove(localAsset);
                    Portfolios.FirstOrDefault(p => p.Id.Equals(SelectedPortfolio.Id))?.Assets?.Remove(portfolioAsset);
                }
                ShowTransactions = false;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error during delete {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task DeleteTransaction(TransactionDto transaction)
        {
            try
            {
                var deletedTransactionAsset = await _transactionService.RemoveTransaction(transaction.Id);
                if (deletedTransactionAsset?.Id != 0)
                {
                    var asset = AssetItemSource.FirstOrDefault(a => a.Id.Equals(deletedTransactionAsset?.Id));
                    var portfolioAsset = Portfolios.FirstOrDefault(p => p.Id.Equals(SelectedPortfolio.Id))?.Assets?.Find(a => a.Id.Equals(deletedTransactionAsset?.Id));
                    if (asset is not null && portfolioAsset is not null)
                    {
                        asset.Amount = deletedTransactionAsset.Amount;
                        asset.AvrgBuyPrice = deletedTransactionAsset.AvrgBuyPrice;
                        asset.Transactions = deletedTransactionAsset.Transactions;
                        portfolioAsset.Amount = deletedTransactionAsset.Amount;
                        portfolioAsset.AvrgBuyPrice = deletedTransactionAsset.AvrgBuyPrice;
                        portfolioAsset.Transactions = deletedTransactionAsset.Transactions;
                    }
                }
                else
                {
                    var assetToDelete = AssetItemSource.FirstOrDefault(a => a.Id.Equals(transaction.AssetId));
                    var portfolioAssetToDelete = Portfolios.FirstOrDefault(p => p.Id.Equals(SelectedPortfolio.Id))?.Assets?.Find(a => a.Id.Equals(transaction.AssetId));
                    if (assetToDelete is not null && portfolioAssetToDelete is not null)
                    {
                        AssetItemSource.Remove(assetToDelete);
                        Portfolios.FirstOrDefault(p => p.Id.Equals(SelectedPortfolio.Id))?.Assets?.Remove(portfolioAssetToDelete);
                    }
                }
                ShowTransactions = false;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error during delete {ex.Message}");
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

        [RelayCommand]
        public void AddTransaction()
        {
            Shell.Current.CurrentPage.ShowPopup(new AddTransactionPopup(new TransactionViewModel(_navigationService, _transactionService, this, (ShowTransactions == true) ? SelectedAsset : null)));
        }

        [RelayCommand]
        public void ShowEditTransactionPopup(TransactionDto transactionToEdit)
        {
            Shell.Current.CurrentPage.ShowPopup(new EditTransactionPopup(new EditTransactionViewModel(_navigationService, _transactionService, this, transactionToEdit)));
        }

        public void StartEditPortfolioPopup()
        {
            Shell.Current.CurrentPage.ShowPopup(new EditPortfolioPopup(new EditPortfolioViewModel(_navigationService, this)));
        }

        [RelayCommand]
        private void Back()
        {
            ShowTransactions = false;
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