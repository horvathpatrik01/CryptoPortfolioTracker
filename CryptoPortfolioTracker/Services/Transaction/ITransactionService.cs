using Shared;

namespace CryptoPortfolioTracker.Services.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>?> GetTransactionsForAsset(int assetId);

        Task<TransactionDto?> GetTransaction(int transactionId);

        Task<AssetDto?> EditTransaction(TransactionDto editedTransactionDto);

        Task<AssetDto?> AddTransaction(TransactionToAddDto transactionToAddDto);

        Task<AssetDto?> RemoveTransaction(int transactionId);
    }
}