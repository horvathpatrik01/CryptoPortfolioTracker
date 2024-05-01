using Shared;

namespace CryptoPortfolioTracker.Services.Portfolio
{
    public interface IPortfolioSevice
    {
        Task<IEnumerable<PortfolioDto>?> GetPortfolios();

        Task<PortfolioDto?> GetPortfolio(int portfolioId);

        Task<PortfolioDto?> EditPortfolio(PortfolioToEditDto portfolioToEdit);

        Task<PortfolioDto?> AddPortfolio(PortfolioToAddDto portfolioToAddDto);

        Task<PortfolioDto?> RemovePortfolio(int portfolioId);
    }
}