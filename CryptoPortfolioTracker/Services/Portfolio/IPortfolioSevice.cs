using Shared;

namespace CryptoPortfolioTracker.Services.Portfolio
{
    public interface IPortfolioSevice
    {
        Task<IEnumerable<PortfolioDto>?> GetPortfolios();

        Task<PortfolioDto?> GetPortfolio(int portfolioId);

        Task<PortfolioDto?> ChangePortfolioName(int portfolioId, string newPortfolioName);

        Task<PortfolioDto?> AddPortfolio(PortfolioToAddDto portfolioToAddDto);

        Task<PortfolioDto?> RemovePortfolio(int portfolioId);
    }
}