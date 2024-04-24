using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views.CreatePortfolioPopup;

public partial class CreatePortfolioTypePopup : Popup
{
    private readonly PortfolioViewModel portfolioViewModel;

    public CreatePortfolioTypePopup(PortfolioViewModel portfolioViewModel)
    {
        InitializeComponent();
        this.portfolioViewModel = portfolioViewModel;
    }

    private async void Close(object? sender, TappedEventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        await CloseAsync(token: cts.Token);
    }

    private async void CreateWalletPortfolio(object sender, TappedEventArgs args)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioWalletPopup(portfolioViewModel));
    }

    private async void CreateExchangePortfolio(object sender, TappedEventArgs args)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioExchangePopup(portfolioViewModel));
    }

    private async void CreateManualPortfolio(object sender, TappedEventArgs args)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioManualPopup(portfolioViewModel));
    }
}