using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Navigation;

namespace CryptoPortfolioTracker.ViewModels.Base
{
    public interface IViewModelBase : IQueryAttributable
    {
        public INavigationService NavigationService { get; }

        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public bool IsBusy { get; }

        public bool IsInitialized { get; }

        Task InitializeAsync();
    }
}