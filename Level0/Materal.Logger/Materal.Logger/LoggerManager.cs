using Microsoft.Extensions.Configuration;

namespace Materal.Logger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public static class LoggerManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="option"></param>
        /// <param name="configuration"></param>
        public static void Init(Action<LoggerConfigOptions>? option = null, IConfiguration? configuration = null)
        {
            LoggerConfigOptions config = new();
            option?.Invoke(config);
            LoggerConfig.Init(config, configuration);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Logger.InitServer();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static void Shutdown() => Logger.Shutdown();
        /// <summary>
        /// 自定义数据
        /// </summary>

        public static Dictionary<string, string> CustomData { get; private set; } = new();
        /// <summary>
        /// 自定义配置
        /// </summary>

        public static Dictionary<string, string> CustomConfig { get; private set; } = new();
        /// <summary>
        /// 程序退出时关闭日志程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e) => Shutdown();
    }
}
