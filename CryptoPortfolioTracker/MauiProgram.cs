﻿using CommunityToolkit.Maui;
using CryptoPortfolioTracker.Services.Auth;
using CryptoPortfolioTracker.Services.Exchange;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.Services.Portfolio;
using CryptoPortfolioTracker.Services.Transaction;
using CryptoPortfolioTracker.Services.User;
using CryptoPortfolioTracker.Services.Wallet;
using CryptoPortfolioTracker.ViewModels;
using CryptoPortfolioTracker.Views;
using MauiIcons.Material;
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
                .UseMauiCommunityToolkit()
                .UseMaterialMauiIcons()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
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
            mauiAppBuilder.Services.AddSingleton<IPortfolioSevice, PortfolioService>();
            mauiAppBuilder.Services.AddSingleton<ITransactionService, TransactionService>();
            mauiAppBuilder.Services.AddSingleton<IExchangeService, ExchangeService>();
            mauiAppBuilder.Services.AddSingleton<IWalletService, WalletService>();

#if DEBUG
            mauiAppBuilder.Logging.AddDebug();
#endif

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
            mauiAppBuilder.Services.AddSingleton<RegisterViewModel>();
            mauiAppBuilder.Services.AddSingleton<PortfolioViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddTransient<RegisterPage>();
            mauiAppBuilder.Services.AddTransient<PortfolioPage>();

            return mauiAppBuilder;
        }
    }
}