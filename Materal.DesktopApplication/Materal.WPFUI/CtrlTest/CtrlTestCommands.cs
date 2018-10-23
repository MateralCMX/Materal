using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest
{
    public class CtrlTestCommands
    {
        /// <summary>
        /// 重新加载控件
        /// </summary>
        public static RoutedUICommand GetViewModelValue { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static CtrlTestCommands()
        {
            GetViewModelValue = new RoutedUICommand(
                "获取ViewModel绑定值",
                nameof(GetViewModelValue),
                typeof(MainWindowCommands),
                new InputGestureCollection
                {
                    new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R")
                });
        }
    }
}
