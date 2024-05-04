using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class AvatarPopup : Popup
{
    public AvatarPopup(AvatarIconViewModel avatarIconViewModel)
    {
        BindingContext = avatarIconViewModel;
        InitializeComponent();
    }

    private async void BackCommand(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }

    private async void SaveAvatar(object sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }
}