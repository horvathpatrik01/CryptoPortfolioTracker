using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.ViewModels.Base;
using CryptoPortfolioTracker.Views.CreatePortfolioPopup;
using Microsoft.Maui.Controls.Shapes;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class CreatePortfolioViewModel : ViewModelBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelectorNotVisible))]
        private bool isSelectorVisible;

        [ObservableProperty]
        private PortfolioToAddDto portfolioToAdd;

        [ObservableProperty]
        private bool walletPortfolioSelected = false;

        [ObservableProperty]
        private int popupHeight;

        [ObservableProperty]
        private bool exchangePortfolioSelected = false;

        [ObservableProperty]
        private string walletAddressLabel;

        private const string defaultAddressLabelText = "Supported networks:  Bitcoin, Ethereum, BNB Smart Chain, Polygon, Arbitrum, Avalanche, Optimism, Fantom, and Cardano.";

        public bool IsSelectorNotVisible => !IsSelectorVisible;

        public CreatePortfolioViewModel(INavigationService navigationService,
                                        PortfolioViewModel portfolioViewModel) : base(navigationService)
        {
            portfolioToAdd = new();
            popupHeight = 440;
            WalletAddressLabel = defaultAddressLabelText;
            isSelectorVisible = true;
            this._portfolioViewModel = portfolioViewModel;
        }

        public void ResetPortfolioToAdd()
        {
            PortfolioToAdd = new PortfolioToAddDto
            {
                Name = "",
                PortfolioType = PortfolioType.Default,
                PublicKey = null,
                PrivateKey = null,
                WalletAddress = null,
                CexIdentifier = null
            };
        }

        [RelayCommand]
        public async Task AddPortfolio()
        {
            await _portfolioViewModel.AddPortfolio(PortfolioToAdd);
        }

        [RelayCommand]
        private void SelectPortfolioType(PortfolioType portfolioType)
        {
            PortfolioToAdd.PortfolioType = portfolioType;

            IsSelectorVisible = false;

            switch (portfolioType)
            {
                case PortfolioType.CexAccount:
                    ExchangePortfolioSelected = true;
                    PopupHeight = 620;
                    break;
                case PortfolioType.Wallet: 
                    WalletPortfolioSelected = true;
                    PopupHeight = 590;
                    break;
                default:
                    PopupHeight = 470; 
                    break;
            }
        }

        [RelayCommand]
        private void Back()
        {
            WalletAddressLabel = defaultAddressLabelText;
            WalletPortfolioSelected = ExchangePortfolioSelected = false;
            IsSelectorVisible = true;
            PopupHeight = 440;
            ResetPortfolioToAdd();
        }
    }
}