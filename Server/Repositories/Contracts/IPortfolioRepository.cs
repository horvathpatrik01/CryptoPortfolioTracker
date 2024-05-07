namespace Server.Repositories.Contracts
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>?> GetPortfolios(Guid? id);

        Task<Portfolio?> GetPortfolio(int? portfolioId);

        Task<Portfolio?> EditPortFolio(PortfolioToEditDto portfolioToEdit);

        Task<Portfolio?> DeletePortfolio(Guid? id, int? portfolioId);

        Task<Portfolio?> AddPortfolio(Guid? id, PortfolioToAddDto portfolioToAddDto);
    }
}