using System;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Controls;

namespace CryptoPortfolioTracker.Converters
{
    public class NumberToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                if (doubleValue <= 0)
                    return Colors.Red;
                if (doubleValue > 0)
                    return Colors.Green;
            }
            return Colors.Black;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}