using DiveHubLogbook.ViewModels;

namespace DiveHubLogbook.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadAsync();
    }

    private async void OnAddDiveClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddDivePage));
    }

    private async void OnLogbookClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LogbookPage));
    }
}