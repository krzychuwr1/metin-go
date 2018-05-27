using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MetinGo.Common;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Converters
{
    public class RarityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Rarity rarity)
            {
                switch (rarity)
                {
                    case Rarity.Normal:
                        return Color.Green;
                    case Rarity.Rare:
                        return Color.Blue;
                    default:
                        return null;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
