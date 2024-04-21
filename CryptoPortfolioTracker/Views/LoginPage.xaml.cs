namespace CryptoPortfolioTracker.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void SignUpTappedRecognizer(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//RegisterPage");
	}
}