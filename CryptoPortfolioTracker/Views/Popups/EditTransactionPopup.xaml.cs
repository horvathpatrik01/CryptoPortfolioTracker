using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class EditTransactionPopup : Popup
{
    public EditTransactionPopup(EditTransactionViewModel editTransactionViewModel)
    {
        BindingContext = editTransactionViewModel;
        InitializeComponent();
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
        Color color, selectedColor;
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
        PPCView.IsVisible = (!(button.Text == "Transfer"));
    }
}