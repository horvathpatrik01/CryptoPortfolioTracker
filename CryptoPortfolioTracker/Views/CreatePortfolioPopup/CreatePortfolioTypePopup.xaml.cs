using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views.CreatePortfolioPopup;

public partial class CreatePortfolioTypePopup : Popup
{
	public CreatePortfolioTypePopup()
	{
		InitializeComponent();
    }

	async void Close(object? sender, EventArgs e)
	{
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        await CloseAsync(token: cts.Token);
    }

    async void CreateWalletPortfolio(object sender, TappedEventArgs args)
    {
       
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioWalletPopup());
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);

    }
    async void CreateExchangePortfolio(object sender, TappedEventArgs args)
    {
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioExchangePopup());
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);

    }
    async void CreateManualPortfolio(object sender, TappedEventArgs args)
    {
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioManualPopup());
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);

    }
}