using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Views;
using Shared.Auth;
using System.Collections.ObjectModel;
using Shared;


namespace CryptoPortfolioTracker.ViewModels
{
    public partial class PortfolioViewModel : ObservableObject
    {

        public ObservableCollection<PortfolioDto> portfolioItemSource { get; set; }
        public ObservableCollection<AssetDto> assetItemSource { get; set; }

        public PortfolioViewModel() {
            portfolioItemSource = new ObservableCollection<PortfolioDto>();



            // Mock data
            portfolioItemSource = new ObservableCollection<PortfolioDto>
            {
                new PortfolioDto { Id = 0 , Name = "Portfolio 1", Icon="user.png", PortfolioType=PortfolioType.Default},
                new PortfolioDto { Id = 1 , Name = "Portfolio 2", Icon = "user.png", PortfolioType = PortfolioType.Default },
                new PortfolioDto { Id = 2 , Name = "Portfolio 3", Icon="user.png", PortfolioType=PortfolioType.Default},
                new PortfolioDto { Id = 3 , Name = "Portfolio 4", Icon = "user.png", PortfolioType = PortfolioType.Default },
                new PortfolioDto { Id = 4 , Name = "Portfolio 5", Icon = "user.png", PortfolioType = PortfolioType.Default }
            };
        }

        public void AddPortfolio(PortfolioDto newPortfolio)
        {
            portfolioItemSource.Add(newPortfolio);
        }

        public void RemovePortfolio(PortfolioDto removedPortfolio)
        {
            portfolioItemSource.Remove(removedPortfolio);
        }

    }

}
