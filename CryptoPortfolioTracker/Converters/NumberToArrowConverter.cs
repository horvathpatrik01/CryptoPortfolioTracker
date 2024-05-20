using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using MauiIcons.Material;
using MauiIcons.Core;

namespace CryptoPortfolioTracker.Converters
{
    public class NumberToArrowConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                if (doubleValue <= 0)
                    return "▼";
                if (doubleValue > 0)
                    return "▲";
            }
            return "";  // No icon for zero or non-integer values
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
