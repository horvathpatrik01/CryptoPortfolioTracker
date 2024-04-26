using CryptoPortfolioTracker.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using CryptoPortfolioTracker.Services.Navigation;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.Views.CreatePortfolioPopup;
using CryptoPortfolioTracker.Services.Auth;

namespace CryptoPortfolioTracker.Views;

public partial class PortfolioPage : ContentPage
{
    private readonly IAuthService authService;

    public PortfolioPage(PortfolioViewModel portfolioPageViewModel)
    {
        InitializeComponent();
        BindingContext = portfolioPageViewModel;
        this.authService = authService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (PortfolioViewModel)BindingContext;

        await viewModel.IsUserAuthenticated();
    }
}