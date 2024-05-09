using CryptoPortfolioTracker.Models;
using Shared;

namespace CryptoPortfolioTracker.Services.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>?> GetTransactionsForAsset(int assetId);

        Task<TransactionDto?> GetTransaction(int transactionId);

        Task<AssetDto?> EditTransaction(TransactionDto editedTransactionDto);

        Task<AssetDto?> AddTransaction(TransactionToAddDto transactionToAddDto, int selectedCoinId);

        Task<AssetDto?> RemoveTransaction(int transactionId);

        Task<AssetDto?> DeleteAsset(int assetId);

        Task<List<Coin>?> GetSupportedCoins();

        Task<List<Coin>?> GetCoinPrices(List<int> ids);
    }
}