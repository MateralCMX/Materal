using System.Windows.Input;
using NCWM.UI.Windows.ConfigSetting;

namespace NCWM.UI.Ctrls.Server
{
    public class ServerCommands
    {
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
        /// 清理控制台文本
        /// </summary>
        public static RoutedUICommand ClearConsoleText { get; }
        /// <summary>
        /// 发送命令
        /// </summary>
        public static RoutedUICommand SendCommand { get; }

        static ServerCommands()
        {
            StartServer = new RoutedUICommand(
                "启动服务",
                nameof(StartServer),
                typeof(ServerCommands));
            ReStartServer = new RoutedUICommand(
                "重启服务",
                nameof(ReStartServer),
                typeof(ServerCommands));
            StopServer = new RoutedUICommand(
                "停止服务",
                nameof(StopServer),
                typeof(ServerCommands));
            ClearConsoleText = new RoutedUICommand(
                "清理控制台文本",
                nameof(ClearConsoleText),
                typeof(ServerCommands));
            SendCommand = new RoutedUICommand(
                "发送命令",
                nameof(SendCommand),
                typeof(ServerCommands));
        }
    }
}
