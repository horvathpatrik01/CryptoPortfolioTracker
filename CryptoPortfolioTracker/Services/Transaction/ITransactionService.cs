using Shared;

namespace CryptoPortfolioTracker.Services.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>?> GetTransactionsForAsset(int assetId);

        Task<TransactionDto?> GetTransaction(int transactionId);

        Task<TransactionDto?> EditTransaction(TransactionDto editedTransactionDto);

        Task<TransactionDto?> AddTransaction(TransactionToAddDto transactionToAddDto);

        Task<TransactionDto?> RemoveTransaction(int transactionId);
    }
}