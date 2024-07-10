using Microsoft.Extensions.Logging;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace Materal.Tools.WinUI.Converters
{
    public class LogLevelToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, string language)
        {
            if (value is not LogLevel logLevel) return value;
            return logLevel switch
            {
                LogLevel.Warning => new SolidColorBrush(Colors.Orange),
                LogLevel.Error or LogLevel.Critical => new SolidColorBrush(Colors.Red),
                _ => new SolidColorBrush(Colors.Black)
            };
        }

        public object? ConvertBack(object? value, Type targetType, object parameter, string language) => value;
    }
}
