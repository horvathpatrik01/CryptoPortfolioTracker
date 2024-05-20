using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class AddTransactionPopup : Popup
{
    private readonly TransactionViewModel _transactionViewModel;

    private Color color, selectedColor;

    public AddTransactionPopup(TransactionViewModel transactionViewModel)
    {
        BindingContext = transactionViewModel;
        InitializeComponent();
        this._transactionViewModel = transactionViewModel;

        if (App.Current?.UserAppTheme == AppTheme.Light)
        {
            color = (Color)App.Current?.Resources["LightSecondary"];
            selectedColor = (Color)App.Current?.Resources["LightPrimary"];
        }
        else
        {
            color = (Color)App.Current?.Resources["DarkPrimary"];
            selectedColor = (Color)App.Current?.Resources["DarkSecondary"];
        }
        buyBtn.BackgroundColor = selectedColor;
    }

    private async void Close(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }

    private async void OnTransactionTypeChanged(object sender, EventArgs e)
    {
        // Show the selected view
        var button = (Button)sender;
        if (App.Current?.UserAppTheme == AppTheme.Light)
        {
            color = (Color)App.Current?.Resources["LightSecondary"];
            selectedColor = (Color)App.Current?.Resources["LightPrimary"];
        }
        else
        {
            color = (Color)App.Current?.Resources["DarkPrimary"];
            selectedColor = (Color)App.Current?.Resources["DarkSecondary"];
        }
        buyBtn.BackgroundColor = color;
        sellBtn.BackgroundColor = color;
        transferBtn.BackgroundColor = color;
        button.BackgroundColor = selectedColor;
        if ((button.Text == "Transfer") && (PPCView.IsVisible))
        {
            await Task.WhenAny<bool>
            (
                PPCView.TranslateTo(0, 15, 250, Easing.CubicIn),
                PPCView.FadeTo(0, 300)
            );
            PPCView.IsVisible = false;
        }
        else if (!(PPCView.IsVisible))
        {
            PPCView.IsVisible = true;
            PPCView.TranslationY = -15;
            await Task.WhenAny<bool>
            (
                PPCView.TranslateTo(0, 0, 250, Easing.CubicOut),
                PPCView.FadeTo(1, 300)
            );
        }
    }
}