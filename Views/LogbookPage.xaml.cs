using DiveHubLogbook.Models;
using DiveHubLogbook.ViewModels;

namespace DiveHubLogbook.Views;

public partial class LogbookPage : ContentPage
{
    private readonly LogbookViewModel _viewModel;
    private bool _isNavigating;

    public LogbookPage(LogbookViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _isNavigating = false;

        await _viewModel.LoadDivesAsync();
    }

    private async void OnDiveSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (_isNavigating)
            return;

        if (e.CurrentSelection.FirstOrDefault() is not Dive dive)
            return;

        _isNavigating = true;

        if (sender is CollectionView collectionView)
        {
            collectionView.SelectedItem = null;
        }

        var navigationParameters =
            new ShellNavigationQueryParameters
            {
                ["DiveId"] = dive.Id
            };

        await Shell.Current.GoToAsync(
            nameof(DiveDetailPage),
            navigationParameters);
    }
}