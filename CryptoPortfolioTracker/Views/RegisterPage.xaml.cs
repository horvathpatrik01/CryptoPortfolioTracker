using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel registerViewModel)
	{
		BindingContext = registerViewModel;
		InitializeComponent();
	}
}