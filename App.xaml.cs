using DiveHubLogbook.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DiveHubLogbook;

public partial class App : Application
{
    private readonly IServiceProvider _services;

    public App(IServiceProvider services)
    {
        InitializeComponent();

        _services = services;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var splashPage = _services.GetRequiredService<SplashPage>();

        return new Window(splashPage);
    }
}