using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Converters
{
    internal class TextToDecimalConverter : BaseConverter<decimal, string>
    {
        public override decimal DefaultConvertBackReturnValue { get; set; } = 0m;
        public override string DefaultConvertReturnValue { get; set; } = string.Empty;

        public override decimal ConvertBackTo(string value, CultureInfo? culture)
        {
            if (value == null)
                return 0;

            // Convert string to decimal
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                return result;

            // If conversion fails, return default value
            return 0;
        }

        public override string ConvertFrom(decimal value, CultureInfo? culture)
        {
            // Convert decimal to string
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}