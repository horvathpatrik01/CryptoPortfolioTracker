using CryptoPortfolioTracker.ViewModels;
using MauiIcons.Core;
using MauiIcons.Material;

namespace CryptoPortfolioTracker.Views;

public partial class PortfolioPage : ContentPageBase
{
    private int clickCounter = 0;

    public PortfolioPage(PortfolioViewModel portfolioPageViewModel)
    {
        BindingContext = portfolioPageViewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (PortfolioViewModel)BindingContext;

        //await viewModel.IsUserAuthenticated();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        clickCounter++;
        if (clickCounter % 2 == 1)
            EditBtn.Icon(MaterialIcons.EditOff);
        else
            EditBtn.Icon(MaterialIcons.Edit);
    }
}