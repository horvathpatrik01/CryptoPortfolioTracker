using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Controls;

namespace CryptoPortfolioTracker.Converters
{
    public class NumberToColorConverter : BaseConverterOneWay<float, Color>
    {
        public override Color DefaultConvertReturnValue { get; set; } =
        Application.Current?.UserAppTheme == AppTheme.Light ? Colors.Black : Colors.White;

        public override Color ConvertFrom(float value, CultureInfo? culture)
        {
            if (value < 0)
                return Colors.Red;
            if (value > 0)
                return Colors.Green;
            return Application.Current?.UserAppTheme == AppTheme.Light ? Colors.Black : Colors.White;
        }
    }
}