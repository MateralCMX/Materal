using System.Windows.Input;

namespace Materal.WPFUI
{
    public class MainWindowCommands
    {
        /// <summary>
        /// 重新加载控件
        /// </summary>
        public static RoutedUICommand ReloadCtrl { get; }
        /// <summary>
        /// 加载数字文本测试控件
        /// </summary>
        public static RoutedUICommand LoadNumberBoxTestCtrl { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static MainWindowCommands()
        {
            ReloadCtrl = new RoutedUICommand(
                "重新加载当前控件",
                nameof(ReloadCtrl),
                typeof(MainWindowCommands),
                new InputGestureCollection
                {
                    new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R")
                });
            LoadNumberBoxTestCtrl = new RoutedUICommand(
                "NumberBox",
                nameof(LoadNumberBoxTestCtrl),
                typeof(MainWindowCommands));
        }
    }
}
