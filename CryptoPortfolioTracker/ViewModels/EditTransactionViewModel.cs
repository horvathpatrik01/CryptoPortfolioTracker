using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Models;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Services.Transaction;
using CryptoPortfolioTracker.ViewModels.Base;
using Shared;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class EditTransactionViewModel : ViewModelBase
    {
        private readonly ITransactionService _transactionService;
        private readonly PortfolioViewModel _portfolioViewModel;

        public ICommand EditTransactionCommand { get; set; }

        [ObservableProperty]
        private TransactionDto editedTransaction;

        public EditTransactionViewModel(INavigationService navigationService,
            ITransactionService transactionService,
            PortfolioViewModel portfolioViewModel,
            TransactionDto selectedTransaction
            ) : base(navigationService)
        {
            this._transactionService = transactionService;
            this._portfolioViewModel = portfolioViewModel;
            EditedTransaction = new()
            {
                Id = selectedTransaction.Id,
                AssetId = selectedTransaction.AssetId,
                Amount = selectedTransaction.Amount,
                Time = selectedTransaction.Time,
                Price = selectedTransaction.Price / selectedTransaction.Amount,
                Fee = selectedTransaction.Fee,
                TransactionType = selectedTransaction.TransactionType,
                Note = selectedTransaction.Note
            };

            EditTransactionCommand = new Command(execute: async () => { await EditTransaction(); });
        }

        [RelayCommand]
        private void ChangeTransactionType(TransactionType transactionType)
        {
            EditedTransaction.TransactionType = transactionType;
        }

        private async Task EditTransaction()
        {
            if (EditedTransaction == null) return;
            EditedTransaction.Price = EditedTransaction.Amount * EditedTransaction.Price;
            var editedTransactionDto = await _transactionService.EditTransaction(EditedTransaction);

            if (editedTransactionDto != null)
            {
                Asset? asset = _portfolioViewModel.AssetItemSource.FirstOrDefault(a => a.Id.Equals(editedTransactionDto.Id));
                AssetDto? assetDto = _portfolioViewModel.SelectedPortfolio.Assets?.Find(a => a.Id.Equals(editedTransactionDto.Id));
                TransactionDto? transaction = _portfolioViewModel.TransactionsSource?.FirstOrDefault(t => t.Id.Equals(EditedTransaction.Id));
                if (asset is not null && assetDto is not null && transaction is not null)
                {
                    asset.AvrgBuyPrice = editedTransactionDto.AvrgBuyPrice;
                    asset.Amount = editedTransactionDto.Amount;
                    asset.Transactions = editedTransactionDto.Transactions;
                    assetDto.AvrgBuyPrice = editedTransactionDto.AvrgBuyPrice;
                    assetDto.Amount = editedTransactionDto.Amount;
                    assetDto.Transactions = editedTransactionDto.Transactions;
                    transaction.TransactionType = EditedTransaction.TransactionType;
                    transaction.Amount = EditedTransaction.Amount;
                    transaction.Fee = EditedTransaction.Fee;
                    transaction.Price = EditedTransaction.Price;
                    transaction.Time = EditedTransaction.Time;
                    transaction.Note = EditedTransaction.Note;
                    _portfolioViewModel.SelectedAsset.AvrgBuyPrice = asset.AvrgBuyPrice;
                    _portfolioViewModel.SelectedAsset.Amount = asset.Amount;
                    _portfolioViewModel.SelectedAsset.Transactions = asset.Transactions;
                }
            }
        }
    }
}