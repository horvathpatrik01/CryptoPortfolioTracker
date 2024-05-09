using CryptoPortfolioTracker.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using CryptoPortfolioTracker.Services.Navigation;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.Views.Popups;
using CryptoPortfolioTracker.Services.Auth;

namespace CryptoPortfolioTracker.Views;

public partial class PortfolioPage : ContentPage
{
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
}