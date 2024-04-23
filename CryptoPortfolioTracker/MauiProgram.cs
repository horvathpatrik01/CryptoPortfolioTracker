using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Services.User;
using CryptoPortfolioTracker.ViewModels;
using CryptoPortfolioTracker.Views;
using Microsoft.Extensions.Logging;

namespace CryptoPortfolioTracker
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews();

            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new AndroidHttpMessageHandler();
#else
                return null;
#endif
            });

            builder.Services.AddHttpClient(AppConstants.HttpClientName, httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7169" : "https://localhost:7169";
                httpClient.BaseAddress = new Uri(baseAddress);
            }).ConfigurePrimaryHttpMessageHandler(builder =>
            {
#if ANDROID
                return builder.GetRequiredService<IPlatformHttpMessageHandler>().GetHttpMessageHandler();
#else
                return new HttpClientHandler();
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
            mauiAppBuilder.Services.AddSingleton<IUserService, UserService>();
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
#if DEBUG
            mauiAppBuilder.Logging.AddDebug();
#endif

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainViewModel>();
            mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
            mauiAppBuilder.Services.AddSingleton<RegisterViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddTransient<RegisterPage>();

            return mauiAppBuilder;
        }
    }
}