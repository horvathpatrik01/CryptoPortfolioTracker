using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views.EditPortfolioPopup;

public partial class EditPortfolioExchangePopup : Popup
{
	public EditPortfolioExchangePopup()
	{
		InitializeComponent();
	}

    private async void OnEntryChange(object? sender, TextChangedEventArgs e)
    {
        if (sender == nameEntry)
            nameEntryCharCount.Text = $"{e.NewTextValue.Length}/32 characters";
        if (nameEntry.Text?.Length > 0 && apiEntry.Text?.Length > 0 && secretEntry.Text?.Length > 0)
            saveButton.IsEnabled = true;
        else
            saveButton.IsEnabled = false;
    }

    private async void Save(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
    }

    private async void Back(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
    }
}