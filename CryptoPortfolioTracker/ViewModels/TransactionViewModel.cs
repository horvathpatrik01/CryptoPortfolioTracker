using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.ViewModels.Base;
using CryptoPortfolioTracker.Views.Popups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class TransactionViewModel : ViewModelBase
    {

        public TransactionViewModel(INavigationService navigationService, PortfolioViewModel portfolioViewModel) : base(navigationService)
        {
        }

    }
}
