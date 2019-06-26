using System.Windows.Input;

namespace NCWM.UI
{
    public class MainWindowCommands
    {
        /// <summary>
        /// 退出
        /// </summary>
        public static RoutedUICommand Exit { get; }
        /// <summary>
        /// 启动服务
        /// </summary>
        public static RoutedUICommand StartServer { get; }
        /// <summary>
        /// 重新启动服务
        /// </summary>
        public static RoutedUICommand ReStartServer { get; }
        /// <summary>
        /// 停止服务
        /// </summary>
        public static RoutedUICommand StopServer { get; }
        /// <summary>
        /// 配置管理
        /// </summary>
        public static RoutedUICommand ConfigSetting { get; }
        /// <summary>
        /// 关于
        /// </summary>
        public static RoutedUICommand About { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static MainWindowCommands()
        {
            Exit = new RoutedUICommand(
                "退出",
                nameof(Exit),
                typeof(MainWindowCommands));
            About = new RoutedUICommand(
                "关于",
                nameof(About),
                typeof(MainWindowCommands));
            StartServer = new RoutedUICommand(
                "启动服务",
                nameof(StartServer),
                typeof(MainWindowCommands));
            ReStartServer = new RoutedUICommand(
                "重启服务",
                nameof(ReStartServer),
                typeof(MainWindowCommands));
            StopServer = new RoutedUICommand(
                "停止服务",
                nameof(StopServer),
                typeof(MainWindowCommands));
            ConfigSetting = new RoutedUICommand(
                "配置管理",
                nameof(ConfigSetting),
                typeof(MainWindowCommands));
        }
    }
}
