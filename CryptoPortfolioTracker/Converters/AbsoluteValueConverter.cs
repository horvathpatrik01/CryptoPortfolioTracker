using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CryptoPortfolioTracker.Converters
{
    public class AbsoluteValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float floatValue)
            {
                // Format the absolute value with two decimal places
                return Math.Abs(floatValue);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
