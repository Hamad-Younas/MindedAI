using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CryptoCoins.Services;
using CryptoCoins.Helpers;

namespace CryptoCoins
{
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
				});

			builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<PackagesService>();


#if DEBUG
            // Add Blazor WebView developer tools and debug logging in DEBUG mode
            builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddDebug();
			});
#endif


			return builder.Build();
		}
	}
}