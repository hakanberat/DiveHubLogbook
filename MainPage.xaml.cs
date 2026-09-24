using DiveHubLogbook.Views;

namespace DiveHubLogbook;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
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