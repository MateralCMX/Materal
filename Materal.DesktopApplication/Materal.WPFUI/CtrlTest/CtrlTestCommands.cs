using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest
{
    public class CtrlTestCommands
    {
        /// <summary>
        /// 获取ViewModel绑定值
        /// </summary>
        public static RoutedUICommand GetViewModelValue { get; }
        /// <summary>
        /// 更新ViewModel绑定值
        /// </summary>
        public static RoutedUICommand UpdateViewModelValue { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static CtrlTestCommands()
        {
            GetViewModelValue = new RoutedUICommand(
                "获取ViewModel绑定值",
                nameof(GetViewModelValue),
                typeof(MainWindowCommands));
            UpdateViewModelValue = new RoutedUICommand(
                "更新ViewModel绑定值",
                nameof(UpdateViewModelValue),
                typeof(MainWindowCommands));
        }
    }
}
