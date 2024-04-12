namespace Server.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions(int? assetId);
        Task<Transaction?> GetTransaction(int? transactionId);
        Task<Transaction?> EditTransaction(TransactionDto transactionToEditDto);
        Task<Transaction?> DeleteTransaction(int? transactionId);
        Task<Transaction?> AddTransaction(TransactionToAddDto transactionToAddDto);
    }
}
