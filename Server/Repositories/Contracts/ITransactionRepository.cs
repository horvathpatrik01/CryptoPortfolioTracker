namespace Server.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions(int? assetId);

        Task<Transaction?> GetTransaction(int? transactionId);

        Task<Asset?> EditTransaction(TransactionDto transactionToEditDto);

        Task DeleteTransaction(int? transactionId);

        Task<Asset?> AddTransaction(TransactionToAddDto transactionToAddDto);
    }
}