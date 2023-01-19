using Microsoft.Extensions.Configuration;

namespace Materal.Logger
{
    public static class MateralLoggerManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="option"></param>
        /// <param name="configuration"></param>
        public static void Init(Action<MateralLoggerConfigOptions>? option = null, IConfiguration? configuration = null)
        {
            MateralLoggerConfigOptions config = new();
            option?.Invoke(config);
            MateralLoggerConfig.Init(config, configuration);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            MateralLogger.InitServer();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static void Shutdown() => MateralLogger.Shutdown();
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
