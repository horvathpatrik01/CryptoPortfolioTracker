namespace CryptoPortfolioTracker.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void SignInTappedRecognizer(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}