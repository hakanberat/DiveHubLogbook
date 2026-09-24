using DiveHubLogbook.ViewModels;

namespace DiveHubLogbook.Views;

public partial class AddDivePage : ContentPage
{
    public AddDivePage(AddDiveViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}