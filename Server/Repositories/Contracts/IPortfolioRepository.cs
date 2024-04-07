namespace Server.Repositories.Contracts
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolio(int? portfolioId);
        Task<Portfolio> EditPortFolioName(int? portfolioId, string newName);
        Task<Portfolio> DeletePortfolio(int? portfolioId);
        Task<Portfolio> AddPortfolio(PortfolioToAddDto portfolioToAddDto);
    }
}
