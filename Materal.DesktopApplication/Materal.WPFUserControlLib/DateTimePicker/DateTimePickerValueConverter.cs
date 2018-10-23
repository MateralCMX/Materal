using System;
using System.Globalization;
using System.Windows.Data;

namespace Materal.WPFUserControlLib.DateTimePicker
{
    public class DateTimePickerValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) throw new DatetTimePickerExeption("参数数目错误");
            if (!(values[0] is DateTime value)) throw new DatetTimePickerExeption("参数错误");
            if (!(values[1] is string format)) throw new DatetTimePickerExeption("参数错误");
            return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
