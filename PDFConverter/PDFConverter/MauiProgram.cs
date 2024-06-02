using Microsoft.Extensions.Logging;
using PdfConverter.ViewModels;
using PdfConverter.Views;
using Plugin.LocalNotification;
using ZXing.Net.Maui;
using Convert = PdfConverter.Views.Convert;

namespace PdfConverter;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
			.UseLocalNotification()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Services.AddSingleton<RewardPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<Convert>();

        builder.Services.AddSingleton<RewardPageViewModel>();
        builder.Services.AddSingleton<LoginViewPageModel>();


#endif
        return builder.Build();
	}
}
