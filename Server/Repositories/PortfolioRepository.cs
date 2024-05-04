using Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext appDbContext;

        public PortfolioRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Portfolio?> AddPortfolio(Guid? id, PortfolioToAddDto portfolioToAddDto)
        {

            User? user= await appDbContext.Users.FindAsync(id);
            
            if(user == null)
            {
                return null;
            }
            Portfolio portfolio = new Portfolio
            {
                UserId = (Guid)id!,
                User = user,
                Name = portfolioToAddDto.Name,
                PortfolioType = portfolioToAddDto.PortfolioType,
            };

            user.Portfolios.Add(portfolio);

            switch (portfolioToAddDto.PortfolioType)
            {
                case PortfolioType.Default:
                    portfolio.Assets = new List<Asset>();
                    portfolio.Icon = "cheese";
                    break;
                case PortfolioType.CexAccount:
                    if(portfolioToAddDto.PublicKey != null && portfolioToAddDto.PrivateKey != null)
                    {
                        portfolio.ApiKey = new ApiKey
                        {
                            PublicKey = portfolioToAddDto.PublicKey,
                            PrivateKey = portfolioToAddDto.PrivateKey,
                            CexIdentifier = portfolioToAddDto.CexIdentifier ?? CexIdentifier.Binance
                        };
                        portfolio.Icon = "ship";
                        break;
                    }
                    return null;
                case PortfolioType.Wallet:
                    if(portfolioToAddDto.WalletAddress is not null)
                    {
                        portfolio.Address = new Address
                        {
                            WalletAddress = portfolioToAddDto.WalletAddress
                        };
                        portfolio.Icon = "auto";
                        break;
                    }
                    return null;
                default:
                    return null;
            }

            var result=await this.appDbContext.AddAsync(portfolio);
            await this.appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Portfolio?> DeletePortfolio(Guid? id, int? portfolioId)
        {
            Portfolio? portfolio = await this.appDbContext.Portfolios.FirstOrDefaultAsync(p => p.Id == portfolioId);

            if (portfolio is not null && portfolio.UserId.Equals(id))
            {
                this.appDbContext.Portfolios.Remove(portfolio);
                await this.appDbContext.SaveChangesAsync();
            }
            return portfolio;
        }

        public async Task<Portfolio?> EditPortFolioName(int? portfolioId, string newName)
        {
            Portfolio? portfolio = await this.appDbContext.Portfolios.FirstOrDefaultAsync(p => p.Id == portfolioId);

            if(portfolio is not null) 
            { 
                portfolio.Name = newName;
                await this.appDbContext.SaveChangesAsync();
            }
            return portfolio;
        }

        public async Task<Portfolio?> GetPortfolio(int? portfolioId)
        {
            return await this.appDbContext.Portfolios
                .Include(p =>p.Address)
                .Include(p=>p.Assets)
                    .ThenInclude(a=>a.Transactions)
                .Include(p =>p.ApiKey)
                .FirstOrDefaultAsync(p => p.Id == portfolioId);
        }

        public async Task<IEnumerable<Portfolio>?> GetPortfolios(Guid? id)
        {
            return await this.appDbContext.Portfolios.Where(p => p.UserId == id)
                .ToListAsync();
        }
    }
}
