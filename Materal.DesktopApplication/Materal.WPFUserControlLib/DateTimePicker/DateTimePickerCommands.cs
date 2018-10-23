using System.Windows.Input;

namespace Materal.WPFUserControlLib.DateTimePicker
{
    public class DateTimePickerCommands
    {
        /// <summary>
        /// 切换Popup
        /// </summary>
        public static readonly RoutedCommand SwitchPopup;
        /// <summary>
        /// 构造方法
        /// </summary>
        static DateTimePickerCommands()
        {
            SwitchPopup = new RoutedCommand(nameof(SwitchPopup), typeof(DateTimePickerCommands));
        }
    }
}
