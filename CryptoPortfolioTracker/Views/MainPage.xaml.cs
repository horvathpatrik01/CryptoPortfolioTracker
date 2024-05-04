using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views;

public partial class MainPage : ContentPage
{
    private readonly IAuthService authService;

    public MainPage(MainViewModel mainPageViewModel,
                    IAuthService authService)
    {
        BindingContext = mainPageViewModel;
        InitializeComponent();
        this.authService = authService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (MainViewModel)BindingContext;
        if (await authService.IsTokenExpired())
        {
            viewModel.LogoutCommand.Execute(this);
        }

        viewModel.GetUserInfoCommand.Execute(this);
    }
}