using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.CreatePortfolioPopup;

public partial class CreatePortfolioManualPopup : Popup
{
    public CreatePortfolioManualPopup()
    {
        InitializeComponent();
    }

    private async void OnEntryChange(object? sender, TextChangedEventArgs e)
    {
        if (sender == nameEntry)
            nameEntryCharCount.Text = $"{e.NewTextValue.Length}/32 characters";
        if (nameEntry.Text?.Length > 0)
            createButton.IsEnabled = true;
        else
            createButton.IsEnabled = false;
    }

    private async void Create(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
    }

    private async void Back(object? sender, EventArgs e)
    {
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioTypePopup());
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
    }
}