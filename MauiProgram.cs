using Microsoft.Extensions.Logging;
using DiveHubLogbook.Data;
using DiveHubLogbook.ViewModels;
using DiveHubLogbook.Views;

namespace DiveHubLogbook;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<DiveDatabase>();
		builder.Services.AddTransient<AddDiveViewModel>();
		builder.Services.AddTransient<AddDivePage>();
		builder.Services.AddTransient<LogbookViewModel>();
		builder.Services.AddTransient<LogbookPage>();	
		builder.Services.AddTransient<DiveDetailViewModel>();
		builder.Services.AddTransient<DiveDetailPage>();
		builder.Services.AddTransient<EditDiveViewModel>();
		builder.Services.AddTransient<EditDivePage>();
		builder.Services.AddTransient<DashboardViewModel>();
		builder.Services.AddTransient<DashboardPage>();
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddTransient<SplashPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
