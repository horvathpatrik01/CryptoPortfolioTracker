using Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext appDbContext;

        public TransactionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Asset?> AddTransaction(TransactionToAddDto transactionToAddDto)
        {
            var portfolio = await this.appDbContext.Portfolios.FindAsync(transactionToAddDto.PortfolioId);

            if (portfolio == null)
            {
                return null;
            }

            var asset = await this.appDbContext.Assets
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == transactionToAddDto.AssetId); // If the transaction added to the query

            if (asset == null)
            {
                asset = await this.appDbContext.Assets
                    .Include(a => a.Transactions)
                    .FirstOrDefaultAsync(a => a.Symbol == transactionToAddDto.Symbol && a.PortfolioId == transactionToAddDto.PortfolioId); // If there is no assetId added to the query

                if (asset == null && transactionToAddDto.Symbol is not null && transactionToAddDto.Name is not null) // If there are no asset in the portfolio
                {
                    asset = new Asset
                    {
                        PortfolioId = transactionToAddDto.PortfolioId,
                        Symbol = transactionToAddDto.Symbol,
                        Name = transactionToAddDto.Name,
                        IconUrl = transactionToAddDto.IconUrl,
                        Portfolio = portfolio,
                        Amount = 0m,
                        AvrgBuyPrice = 0m
                    };
                    var assetResult = await this.appDbContext.Assets.AddAsync(asset);
                    if (assetResult == null)
                        return null;
                }
            }

            var transaction = new Transaction
            {
                Amount = transactionToAddDto.Amount,
                Asset = asset!,
                AssetId = asset!.Id,
                Fee = transactionToAddDto.Fee,
                Note = transactionToAddDto.Note,
                Time = transactionToAddDto.Time,
                Price = transactionToAddDto.Price,
                TransactionType = transactionToAddDto.TransactionType,
            };

            if (new[] { TransactionType.Buy, TransactionType.TransferIn }.Contains(transactionToAddDto.TransactionType))
                asset.Amount += transactionToAddDto.Amount;
            else
                transaction.Asset.Amount -= transaction.Amount;

            var result = await this.appDbContext.Transactions.AddAsync(transaction);
            transaction = result?.Entity;
            if (transaction == null)
                return null;
            asset.AvrgBuyPrice = CalculateAverageBuyPrice(asset.Transactions);

            await this.appDbContext.SaveChangesAsync();
            return asset;
        }

        public async Task DeleteTransaction(int? transactionId)
        {
            var transaction = await this.appDbContext.Transactions
                .Include(t => t.Asset)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction is not null)
            {
                transaction.Asset.Transactions.Remove(transaction);
                this.appDbContext.Transactions.Remove(transaction);
                if (transaction.Asset.Transactions.Count == 0)
                {
                    this.appDbContext.Assets.Remove(transaction.Asset);
                    await this.appDbContext.SaveChangesAsync();

                    return;
                }

                if (new[] { TransactionType.Buy, TransactionType.TransferIn }.Contains(transaction.TransactionType))
                    transaction.Asset.Amount -= transaction.Amount;
                else
                    transaction.Asset.Amount += transaction.Amount;

                transaction.Asset.AvrgBuyPrice = CalculateAverageBuyPrice(transaction.Asset.Transactions);
                await this.appDbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task<Asset?> EditTransaction(TransactionDto transactionToEditDto)
        {
            var t = await this.appDbContext.Transactions
                .Include(t => t.Asset)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefaultAsync(t => t.Id == transactionToEditDto.Id);

            if (t is null)
            {
                return null;
            }

            if (new[] { TransactionType.Buy, TransactionType.TransferIn }.Contains(t.TransactionType))
                t.Asset.Amount -= t.Amount;
            else
                t.Asset.Amount += t.Amount;

            t.Time = transactionToEditDto.Time;
            t.Price = transactionToEditDto.Price;
            t.Fee = transactionToEditDto.Fee;
            t.Note = transactionToEditDto.Note;
            t.Amount = transactionToEditDto.Amount;
            t.TransactionType = transactionToEditDto.TransactionType;

            if (new[] { TransactionType.Buy, TransactionType.TransferIn }.Contains(t.TransactionType))
                t.Asset.Amount += t.Amount;
            else
                t.Asset.Amount -= t.Amount;

            t.Asset.AvrgBuyPrice = CalculateAverageBuyPrice(t.Asset.Transactions);

            await this.appDbContext.SaveChangesAsync();
            return t.Asset;
        }

        public async Task<Transaction?> GetTransaction(int? transactionId)
        {
            return await this.appDbContext.Transactions.FindAsync(transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(int? assetId)
        {
            return await this.appDbContext.Transactions.Where(t => t.AssetId == assetId).ToListAsync();
        }

        private decimal CalculateAverageBuyPrice(List<Transaction> transactions)
        {
            var Cost = transactions.Where(t => t.TransactionType == TransactionType.Buy).Sum(t => t.Price);
            var Amount = transactions.Where(t => t.TransactionType == TransactionType.Buy).Sum(t => t.Amount);

            return Amount == 0m ? 0m : Cost / Amount;
        }
    }
}