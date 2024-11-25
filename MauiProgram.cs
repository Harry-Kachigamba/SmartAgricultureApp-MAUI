using Microsoft.Extensions.Logging;

namespace SmartAgricultureApp_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        //Set environment variables
        Environment.SetEnvironmentVariable("SUPABASE_URL", "xxxxx");
        Environment.SetEnvironmentVariable("SUPABASE_KEY", "xxxxx");

        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}