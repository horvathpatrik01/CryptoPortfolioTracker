using CryptoPortfolioTracker.ViewModels;
using MauiIcons.Core;
using MauiIcons.Material;

namespace CryptoPortfolioTracker.Views;

public partial class PortfolioPage : ContentPageBase
{
    private readonly PortfolioViewModel portfolioPageViewModel;


    public PortfolioPage(PortfolioViewModel portfolioPageViewModel)
    {
        BindingContext = portfolioPageViewModel;
        InitializeComponent();
        this.portfolioPageViewModel = portfolioPageViewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (portfolioPageViewModel.IsEditing)
            EditBtn.Icon(MaterialIcons.EditOff);
        else
            EditBtn.Icon(MaterialIcons.Edit);
    }
}