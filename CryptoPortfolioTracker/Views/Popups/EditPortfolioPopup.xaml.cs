using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.ViewModels;
using System.Text.RegularExpressions;

namespace CryptoPortfolioTracker.Views.Popups;

public partial class EditPortfolioPopup : Popup
{
    public EditPortfolioPopup(EditPortfolioViewModel editPortfolioViewModel)
    {
        InitializeComponent();
        BindingContext = editPortfolioViewModel;
    }

    private async void OnEntryChange(object? sender, TextChangedEventArgs e)
    {
        if (sender == nameEntry)
            nameEntryCharCount.Text = $"{e.NewTextValue.Length}/32 characters";
        if (nameEntry.Text?.Length > 0)
            saveButton.IsEnabled = true;
        else
            saveButton.IsEnabled = false;
    }


    private async void Close(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(token: cts.Token);
        cts.Dispose();
    }


    private void ChangeAvatar(object sender, EventArgs e)
    {   
        
        Shell.Current.CurrentPage.ShowPopup(new AvatarPopup());
    }

}