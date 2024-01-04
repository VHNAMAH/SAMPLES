using Microsoft.AspNetCore.Components.WebView.Maui;
using DC2023.Data;

namespace DC2023;

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
				fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
            });

		builder.Services.AddMauiBlazorWebView();

		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
