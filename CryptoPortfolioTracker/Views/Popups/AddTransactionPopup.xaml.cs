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
        var button = (Button)sender;
        Color lightPrimaryColor = (Color)App.Current?.Resources["LightPrimary"];
        Color lightSecondaryColor = (Color)App.Current.Resources["LightSecondary"];
        Color darkPrimaryColor = (Color)App.Current.Resources["DarkPrimary"];
        Color darkSecondaryColor = (Color)App.Current.Resources["DarkSecondary"];
        switch (button.Text)
        {
            case "Buy":
                PPCView.IsVisible = true;
                sellBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                transferBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                break;

            case "Sell":
                PPCView.IsVisible = true;
                buyBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                transferBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                break;

            case "Transfer":
                PPCView.IsVisible = false;
                sellBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                buyBtn.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightSecondaryColor : darkPrimaryColor;
                break;
        }
        button.TextColor = App.Current?.UserAppTheme == AppTheme.Light ? lightPrimaryColor : darkSecondaryColor;
    }
}