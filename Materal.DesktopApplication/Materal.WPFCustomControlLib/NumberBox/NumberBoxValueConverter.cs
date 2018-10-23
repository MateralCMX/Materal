using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using Materal.StringHelper;

namespace Materal.WPFCustomControlLib.NumberBox
{

    public class NumberBoxValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue) return decimalValue.ToString(CultureInfo.InvariantCulture);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue)) return 0m;
            if (stringValue.IsNumber())
            {
                return System.Convert.ToDecimal(stringValue);
            }
            MatchCollection matchCollection = stringValue.GetNumberInStr();
            return matchCollection.Count > 0 ? System.Convert.ToDecimal(matchCollection[0].Value) : 0m;
        }
    }
}
