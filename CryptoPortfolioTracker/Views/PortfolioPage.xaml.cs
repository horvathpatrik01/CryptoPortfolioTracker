using CryptoPortfolioTracker.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using CryptoPortfolioTracker.Services.Navigation;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.Views.Popups;
using CryptoPortfolioTracker.Services.Auth;
using MauiIcons.Core;
using MauiIcons.Material;

namespace CryptoPortfolioTracker.Views;

public partial class PortfolioPage : ContentPage
{
    private int clickCounter = 0;
    public PortfolioPage(PortfolioViewModel portfolioPageViewModel)
    {
        InitializeComponent();
        BindingContext = portfolioPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (PortfolioViewModel)BindingContext;

        await viewModel.IsUserAuthenticated();
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