namespace Materal.Logger
{
    public static class MateralLoggerManager
    {
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
    }
}
