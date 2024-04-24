using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.CreatePortfolioPopup;

public partial class CreatePortfolioWalletPopup : Popup
{
   
    public CreatePortfolioWalletPopup()
	{
		InitializeComponent();
	}

    async void OnEntryChange(object? sender, TextChangedEventArgs e)
    {
        if (sender == nameEntry)
            nameEntryCharCount.Text = $"{e.NewTextValue.Length}/32 characters";
        if (nameEntry.Text?.Length > 0 && addressEntry.Text?.Length > 0)
            createButton.IsEnabled = true;
        else
            createButton.IsEnabled = false;
    }

    async void Create(object? sender, EventArgs e)
    {

        if (!Regex.IsMatch(addressEntry.Text, "^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,39}$|^0x[a-fA-F0-9]{40}$"))
        {
            addressEntryLabel.Text = "This address is invalid or not supported. Please connect an other wallet.";
            addressEntryLabel.TextColor = Colors.Red;
        }
        else
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await CloseAsync(token: cts.Token);
        }
    }
    async void Back(object? sender, EventArgs e)
    {
        Shell.Current.CurrentPage.ShowPopup(new CreatePortfolioTypePopup());
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
    }

}