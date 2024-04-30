using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class CreateTransactionPopup : Popup
{

    public CreateTransactionPopup()
    {
        InitializeComponent();
    }

    private async void Close(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }

    private async void OnEntryChange(object? sender, TextChangedEventArgs e)
    {

    }


}