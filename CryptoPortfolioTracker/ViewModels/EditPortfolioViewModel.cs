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

        [ObservableProperty]
        private int newPortfolioIconColor = 0;

        public EditPortfolioViewModel(INavigationService navigationService,
            PortfolioViewModel portfolioViewModel) : base(navigationService)
        {
            this._portfolioId = portfolioViewModel.SelectedPortfolio.Id;
            NewPortfolioName = portfolioViewModel.SelectedPortfolio.Name;
            NewPortfolioIcon = portfolioViewModel.SelectedPortfolio.Icon;
            newPortfolioIconColor = portfolioViewModel.SelectedPortfolio.IconColor;
            this._portfolioViewModel = portfolioViewModel;
        }

        [RelayCommand]
        private async Task EditPortfolio()
        {
            PortfolioToEditDto portfolioToEdit = new PortfolioToEditDto
            {
                Id = this._portfolioId,
                Name = NewPortfolioName,
                Icon = NewPortfolioIcon,
                IconColor = NewPortfolioIconColor
            };
            await _portfolioViewModel.EditPortfolio(portfolioToEdit);
        }

        [RelayCommand]
        private void ChangeAvatar()
        {
            Shell.Current.CurrentPage.ShowPopup(new AvatarPopup(new AvatarIconViewModel(editPortfolioViewModel: this)));
        }
    }
}