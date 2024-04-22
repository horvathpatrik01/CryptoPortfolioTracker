using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		BindingContext = loginViewModel;
		InitializeComponent();
	}
}