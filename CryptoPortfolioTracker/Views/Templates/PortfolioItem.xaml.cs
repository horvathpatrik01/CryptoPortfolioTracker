using CryptoPortfolioTracker.ViewModels;
using Shared;

namespace CryptoPortfolioTracker.Views.Templates;

public partial class PortfolioItem : ContentView
{
    public PortfolioItem()
    {
        InitializeComponent();
    }



    private void OnDeleteTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Label label && label.BindingContext is PortfolioDto item)
        {
            // Find the parent view with PortfolioViewModel as BindingContext
            var parent = label.Parent;
            PortfolioViewModel viewModel = null;

            while (parent != null)
            {
                if (parent.BindingContext is PortfolioViewModel model)
                {
                    viewModel = model;
                    break;
                }
                parent = parent.Parent;
            }
            // Execute the RemovePortfolioCommand with the item as the command parameter
            if (viewModel is not null && viewModel.RemovePortfolioCommand != null)
            {
                viewModel.RemovePortfolioCommand.Execute(item);
            }
        }
    }

    private void OnEditTapped(object sender, EventArgs e)
    {
        // Get the data context of the tapped item
        if (sender is Label label)
        {
            // Find the parent view with PortfolioViewModel as BindingContext
            var parent = label.Parent;
            PortfolioViewModel viewModel = null;

            while (parent != null)
            {
                if (parent.BindingContext is PortfolioViewModel model)
                {
                    viewModel = model;
                    break;
                }
                parent = parent.Parent;
            }
            // Execute the RemovePortfolioCommand with the item as the command parameter
            if (viewModel is not null && viewModel.RemovePortfolioCommand != null)
            {
                viewModel.StartEditPortfolioPopup();
            }
        }
    }
}