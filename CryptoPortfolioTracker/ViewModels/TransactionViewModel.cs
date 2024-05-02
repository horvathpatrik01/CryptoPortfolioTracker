using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Services.Transaction;
using CryptoPortfolioTracker.ViewModels.Base;
using CryptoPortfolioTracker.Views.Popups;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class TransactionViewModel : ViewModelBase
    {
        private readonly ITransactionService _transactionService;
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly AssetDto? _selectedAsset;

        [ObservableProperty]
        private TransactionToAddDto _transactionToAddDto;

        public TransactionViewModel(
            INavigationService navigationService,
            ITransactionService transactionService,
            PortfolioViewModel portfolioViewModel,
            AssetDto? selectedAsset = null) : base(navigationService)
        {
            this._transactionService = transactionService;
            this._portfolioViewModel = portfolioViewModel;
            this._selectedAsset = selectedAsset;
            TransactionToAddDto = new TransactionToAddDto();
            ResetTransactionProperties();
        }

        private void ResetTransactionProperties()
        {
            TransactionToAddDto.PortfolioId = _portfolioViewModel.SelectedPortfolio.Id;
            TransactionToAddDto.Time = DateTime.Now;
            TransactionToAddDto.Fee = 0;
            TransactionToAddDto.Price = 0;
            TransactionToAddDto.Amount = 0;
            TransactionToAddDto.Note = string.Empty;
            if (_selectedAsset is not null)
            {
                TransactionToAddDto.AssetId = _selectedAsset.Id;
                TransactionToAddDto.Symbol = _selectedAsset.Symbol;
                TransactionToAddDto.Name = _selectedAsset.Name;
                TransactionToAddDto.IconUrl = _selectedAsset.IconUrl;
            }
        }

        [RelayCommand]
        private void ChangeTransactionType(TransactionType transactionType)
        {
            ResetTransactionProperties();
            TransactionToAddDto.TransactionType = transactionType;
        }

        [RelayCommand]
        private async Task AddTransaction()
        {
            TransactionToAddDto.Symbol = "BTC";
            TransactionToAddDto.Name = "Bitcoin";
            TransactionToAddDto.IconUrl = "dotnet_bot";
            // Server stores the TotalPrice
            TransactionToAddDto.Price = TransactionToAddDto.Amount * TransactionToAddDto.Price;
            var assetWithNewTransaction = await _transactionService.AddTransaction(TransactionToAddDto);
            if (assetWithNewTransaction != null)
            {
                AssetDto? asset = _portfolioViewModel.AssetItemSource.FirstOrDefault(a => a.Id.Equals(assetWithNewTransaction.Id));
                if (asset is not null)
                {
                    asset.AvrgBuyPrice = assetWithNewTransaction.AvrgBuyPrice;
                    asset.Amount = assetWithNewTransaction.Amount;
                    asset.Transactions = assetWithNewTransaction.Transactions;
                }
                else
                    _portfolioViewModel.AssetItemSource.Add(assetWithNewTransaction);
            }
        }
    }
}