using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.ViewModels.Base;
using CryptoPortfolioTracker.Views.Popups;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class EditPortfolioViewModel : ViewModelBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly int _portfolioId;

        [ObservableProperty]
        private string newPortfolioName = "";

        [ObservableProperty]
        private string newPortfolioIcon = "";

        public EditPortfolioViewModel(INavigationService navigationService,
            PortfolioViewModel portfolioViewModel) : base(navigationService)
        {
            this._portfolioId = portfolioViewModel.SelectedPortfolio.Id;
            NewPortfolioName = portfolioViewModel.SelectedPortfolio.Name;
            NewPortfolioIcon = portfolioViewModel.SelectedPortfolio.Icon;
            this._portfolioViewModel = portfolioViewModel;
        }

        [RelayCommand]
        private async Task EditPortfolio()
        {
            await _portfolioViewModel.EditPortfolio(_portfolioId, NewPortfolioName, NewPortfolioIcon);
        }

    }
}