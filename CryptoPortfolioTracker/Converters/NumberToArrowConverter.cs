using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using MauiIcons.Material;

namespace CryptoPortfolioTracker.Converters
{
    public class NumberToArrowConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue < 0)
                    return MaterialIcons.ArrowDropDown;
                if (intValue > 0)
                    return MaterialIcons.ArrowDropUp;
            }
            return null;  // No icon for zero or non-integer values
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
