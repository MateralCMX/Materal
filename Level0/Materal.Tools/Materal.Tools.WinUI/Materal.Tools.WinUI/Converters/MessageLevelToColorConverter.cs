using Materal.Tools.Core;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace Materal.Tools.WinUI.Converters
{
    public class MessageLevelToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, string language)
        {
            if (value is not MessageLevel messageLevel) return value;
            return messageLevel switch
            {
                MessageLevel.Information => new SolidColorBrush(Colors.Black),
                MessageLevel.Warning => new SolidColorBrush(Colors.Orange),
                MessageLevel.Error => new SolidColorBrush(Colors.Red),
                _ => value
            };
        }

        public object? ConvertBack(object? value, Type targetType, object parameter, string language) => value;
    }
}
