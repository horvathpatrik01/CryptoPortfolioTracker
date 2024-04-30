using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;

using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class AddTransactionPopup : Popup
{
    private readonly TransactionViewModel _transactionViewModel;
    public AddTransactionPopup(TransactionViewModel transactionViewModel)
    {
        BindingContext = transactionViewModel;
        InitializeComponent();
        this._transactionViewModel = transactionViewModel;
    }

    private async void Close(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }

    private void OnTransactionTypeChanged(object sender, EventArgs e)
    {
        // Show the selected view
        switch (button.Text)
        {
            case ("Buy" or "Sell"):
                PPCView.IsVisible = true;
                break;
            case "Transfer":
                PPCView.IsVisible = false;
                break;
        }
    }
}