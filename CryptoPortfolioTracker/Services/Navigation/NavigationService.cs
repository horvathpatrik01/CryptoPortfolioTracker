using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
        }

        public async Task InitializeAsync()
        {
            if (!string.IsNullOrEmpty(await AuthService.GetAuthToken()))
                await NavigateToAsync($"{nameof(MainPage)}");
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            var shellNavigation = new ShellNavigationState(route);

            return routeParameters != null
                ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        public Task PopAsync() =>
            Shell.Current.GoToAsync("..");
    }
}