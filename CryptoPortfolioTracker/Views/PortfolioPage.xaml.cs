using CryptoPortfolioTracker.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using CryptoPortfolioTracker.Services.Navigation;
using CommunityToolkit.Maui.Views;
using CryptoPortfolioTracker.Views.CreatePortfolioPopup;


namespace CryptoPortfolioTracker.Views;
public partial class PortfolioPage : ContentPage
{
    public PortfolioPage(PortfolioViewModel portfolioPageViewModel)
    {
        BindingContext = portfolioPageViewModel;
        InitializeComponent();
    }

    public void ChangeTheme(object sender, TappedEventArgs args)
    {
        Application.Current.UserAppTheme = Application.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
    }

    public async void CreatePortfolio(object sender, TappedEventArgs args)
    {
        this.ShowPopup(new CreatePortfolioTypePopup());

        //await Navigation.PopModalAsync();
    }

}
