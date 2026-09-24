using DiveHubLogbook.ViewModels;

namespace DiveHubLogbook.Views;

public partial class DiveDetailPage :
    ContentPage,
    IQueryAttributable
{
    private readonly DiveDetailViewModel _viewModel;

    private int _diveId;

    public DiveDetailPage(DiveDetailViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        if (!query.TryGetValue("DiveId", out var value))
            return;

        if (value is int id)
        {
            _diveId = id;
            return;
        }

        if (int.TryParse(value?.ToString(), out id))
        {
            _diveId = id;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_diveId <= 0)
            return;

        await _viewModel.LoadDiveAsync(_diveId);
    }

    private async void OnEditClicked(
        object? sender,
        EventArgs e)
    {
        if (_viewModel.Dive is null)
            return;

        var navigationParameters =
            new ShellNavigationQueryParameters
            {
                ["DiveId"] = _viewModel.Dive.Id
            };

        await Shell.Current.GoToAsync(
            nameof(EditDivePage),
            navigationParameters);
    }

    private async void OnDeleteClicked(
        object? sender,
        EventArgs e)
    {
        if (_viewModel.Dive is null)
            return;

        bool confirmed = await DisplayAlertAsync(
            "Delete Dive",
            "Are you sure you want to delete this dive?",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        await _viewModel.DeleteDiveAsync();

        await Shell.Current.GoToAsync("..");
    }
}