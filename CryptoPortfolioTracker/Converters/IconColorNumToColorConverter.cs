using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Converters
{
    public class IconColorNumToColorConverter : BaseConverter<int?, Color>
    {
        private static Color[] _colors =
            {
                Color.FromArgb("9146FF"),
                Color.FromArgb("4BF5DC"),
                Color.FromArgb("28D7FA"),
                Color.FromArgb("FAB478"),
                Color.FromArgb("FA7D64"),
                Color.FromArgb("F03C4B"),
                Color.FromArgb("8791A0"),
                Color.FromArgb("50AA5A")
            };

        public override Color DefaultConvertReturnValue { get; set; } = _colors[0];

        public override int? DefaultConvertBackReturnValue { get; set; } = 0;

        public override Color ConvertFrom(int? value, CultureInfo? culture)
        {
            if (value is not null && value >= 0 && value < _colors.Length)
            {
                return _colors[(int)value];
            }
            return DefaultConvertReturnValue;
        }

        public override int? ConvertBackTo(Color value, CultureInfo? culture)
        {
            int index = Array.IndexOf(_colors, value);
            return index != -1 ? index : DefaultConvertBackReturnValue;
        }
    }
}