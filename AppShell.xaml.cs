using DiveHubLogbook.Views;

namespace DiveHubLogbook;

public partial class AppShell : Shell
{
    public AppShell(DashboardPage dashboardPage)
    {
        InitializeComponent();

        Items.Add(new ShellContent
        {
            Title = "Dashboard",
            Route = "Dashboard",
            Content = dashboardPage
        });

        Routing.RegisterRoute(nameof(AddDivePage), typeof(AddDivePage));
        Routing.RegisterRoute(nameof(LogbookPage), typeof(LogbookPage));
        Routing.RegisterRoute(nameof(DiveDetailPage), typeof(DiveDetailPage));
        Routing.RegisterRoute(nameof(EditDivePage), typeof(EditDivePage));
    }
}