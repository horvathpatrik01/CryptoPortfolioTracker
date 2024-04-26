using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.CreatePortfolioPopup;

public partial class CreatePortfolioPopup : Popup
{
    private readonly CreatePortfolioViewModel _createPortfolioViewModel;

    public CreatePortfolioPopup(CreatePortfolioViewModel createPortfolioViewModel)
    {
        BindingContext = createPortfolioViewModel;
        InitializeComponent();
        this._createPortfolioViewModel = createPortfolioViewModel;
    }

    private async void Close(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        await CloseAsync(token: cts.Token);
    }

    private void OnEntryChange(object? sender, TextChangedEventArgs e)
    {
        bool regex = false;
        if (sender == nameEntry)
            nameEntryCharCount.Text = $"{e.NewTextValue.Length}/32 characters";
        else if (sender == addressEntry && addressEntry.Text is not null)
        {
            if (Regex.IsMatch(addressEntry.Text, "^(bc1|[13])[a-zA-HJ-NP-Z0-9]{25,39}$|^0x[a-fA-F0-9]{40}$"))
            {
                regex = true;
                addressEntryLabel.Text = "";
            }
            else
            {
                regex = false;
                addressEntryLabel.Text = "This address is invalid or not supported. Please connect an other wallet.";
            }
        }
        if (nameEntry.Text?.Length > 0 && (regex || !_createPortfolioViewModel.WalletPortfolioSelected) && ((apiEntry.Text?.Length > 0 && secretEntry.Text?.Length > 0) || !_createPortfolioViewModel.ExchangePortfolioSelected))
            createButton.IsEnabled = true;
        else
            createButton.IsEnabled = false;
    }
}