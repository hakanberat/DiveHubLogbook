using DiveHubLogbook.ViewModels;

namespace DiveHubLogbook.Views;

public partial class EditDivePage :
    ContentPage,
    IQueryAttributable
{
    private readonly EditDiveViewModel _viewModel;

    public EditDivePage(EditDiveViewModel viewModel)
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

        int id;

        if (value is int intValue)
        {
            id = intValue;
        }
        else if (!int.TryParse(value?.ToString(), out id))
        {
            return;
        }

        MainThread.BeginInvokeOnMainThread(
            async () =>
            {
                await _viewModel.LoadDiveAsync(id);
            });
    }
}