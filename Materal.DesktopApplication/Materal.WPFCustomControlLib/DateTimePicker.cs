using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFCustomControlLib
{
    public class DateTimePicker : DatePicker
    {
        static DateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
        }
    }
}
