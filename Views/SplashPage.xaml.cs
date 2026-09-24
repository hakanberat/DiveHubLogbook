namespace DiveHubLogbook.Views;

public partial class SplashPage : ContentPage
{
    private readonly AppShell _appShell;
    private bool _hasStarted;

    public SplashPage(AppShell appShell)
    {
        InitializeComponent();

        _appShell = appShell;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_hasStarted)
            return;

        _hasStarted = true;

        await Task.Delay(1500);

        if (Window is not null)
        {
            Window.Page = _appShell;
        }
    }
}