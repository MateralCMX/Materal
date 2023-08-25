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
        /// <param name="configuration"></param>
        public static void Init(IConfiguration? configuration = null)
        {
            LoggerConfig.Init(configuration);
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => Logger.ShutdownAsync().Wait();
        }
        /// <summary>
        /// 自定义数据
        /// </summary>

        public static Dictionary<string, string> CustomData => LoggerConfig.CustomData;
        /// <summary>
        /// 自定义配置
        /// </summary>

        public static Dictionary<string, string> CustomConfig => LoggerConfig.CustomConfig;
    }
}
