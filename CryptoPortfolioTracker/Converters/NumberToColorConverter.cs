using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CryptoPortfolioTracker.Converters
{
    public class NumberToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue < 0)
                    return Colors.Red;
                if (intValue > 0)
                    return Colors.Green;
                return Application.Current.UserAppTheme == AppTheme.Light ? Colors.Black : Colors.White;


            }

            return Application.Current.UserAppTheme == AppTheme.Light ? Colors.Black : Colors.White; // Default color if value is not an integer
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
