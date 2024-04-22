using CryptoPortfolioTracker.ViewModels;

namespace CryptoPortfolioTracker.Views;
public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage(MainViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
