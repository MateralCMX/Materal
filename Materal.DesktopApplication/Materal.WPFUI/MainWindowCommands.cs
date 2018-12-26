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
        /// 加载测试控件
        /// </summary>
        public static RoutedUICommand LoadTestCtrl { get; }
        /// <summary>
        /// 加载查询框测试控件
        /// </summary>
        public static RoutedUICommand LoadSearchBoxTestCtrl { get; }
        /// <summary>
        /// 加载数字测试控件
        /// </summary>
        public static RoutedUICommand LoadNumberBoxTestCtrl { get; }
        /// <summary>
        /// 加载日期时间测试控件
        /// </summary>
        public static RoutedUICommand LoadDateTimePickerTestCtrl { get; }
        /// <summary>
        /// 加载网页浏览器测试控件
        /// </summary>
        public static RoutedUICommand LoadWebBrowserTestCtrl { get; }
        /// <summary>
        /// 加载网页浏览器测试控件
        /// </summary>
        public static RoutedUICommand LoadCefSharpTestCtrl { get; }
        /// <summary>
        /// 加载网页浏览器测试控件
        /// </summary>
        public static RoutedUICommand LoadEdgeTestCtrl { get; }
        /// <summary>
        /// 加载圆角按钮测试控件
        /// </summary>
        public static RoutedUICommand LoadCornerRadiusButtonTestCtrl { get; }
        /// <summary>
        /// 加载圆角选择按钮测试控件
        /// </summary>
        public static RoutedUICommand LoadCornerRadiusToggleButtonTestCtrl { get; }
        /// <summary>
        /// 加载圆角文本框测试控件
        /// </summary>
        public static RoutedUICommand LoadCornerRadiusTextBoxTestCtrl { get; }
        /// <summary>
        /// 加载NuGet工具控件
        /// </summary>
        public static RoutedUICommand LoadNuGetToolsCtrl { get; }
        /// <summary>
        /// 检查更新
        /// </summary>
        public static RoutedUICommand CheckForUpdates { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static MainWindowCommands()
        {
            CheckForUpdates = new RoutedUICommand(
                "检查更新",
                nameof(CheckForUpdates),
                typeof(MainWindowCommands));
            ReloadCtrl = new RoutedUICommand(
                "重新加载当前控件",
                nameof(ReloadCtrl),
                typeof(MainWindowCommands),
                new InputGestureCollection
                {
                    new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R")
                });
            LoadTestCtrl = new RoutedUICommand(
                "测试控件",
                nameof(LoadTestCtrl),
                typeof(MainWindowCommands));
            LoadSearchBoxTestCtrl = new RoutedUICommand(
                "SearchBox",
                nameof(LoadSearchBoxTestCtrl),
                typeof(MainWindowCommands));
            LoadNumberBoxTestCtrl = new RoutedUICommand(
                "NumberBox",
                nameof(LoadNumberBoxTestCtrl),
                typeof(MainWindowCommands));
            LoadDateTimePickerTestCtrl = new RoutedUICommand(
                "DateTimePicker",
                nameof(LoadDateTimePickerTestCtrl),
                typeof(MainWindowCommands));
            LoadWebBrowserTestCtrl = new RoutedUICommand(
                "WebBrowser",
                nameof(LoadWebBrowserTestCtrl),
                typeof(MainWindowCommands));
            LoadCefSharpTestCtrl = new RoutedUICommand(
                "CefSharp",
                nameof(LoadCefSharpTestCtrl),
                typeof(MainWindowCommands));
            LoadEdgeTestCtrl = new RoutedUICommand(
                "Edge",
                nameof(LoadEdgeTestCtrl),
                typeof(MainWindowCommands));
            LoadNuGetToolsCtrl = new RoutedUICommand(
                "NuGetTools",
                nameof(LoadNuGetToolsCtrl),
                typeof(MainWindowCommands));
            LoadCornerRadiusTextBoxTestCtrl = new RoutedUICommand(
                "CornerRadiusTextBox",
                nameof(LoadCornerRadiusTextBoxTestCtrl),
                typeof(MainWindowCommands));
            LoadCornerRadiusButtonTestCtrl = new RoutedUICommand(
                "CornerRadiusButton",
                nameof(LoadCornerRadiusButtonTestCtrl),
                typeof(MainWindowCommands));
            LoadCornerRadiusToggleButtonTestCtrl = new RoutedUICommand(
                "CornerRadiusToggleButton",
                nameof(LoadCornerRadiusToggleButtonTestCtrl),
                typeof(MainWindowCommands));
        }
    }
}
