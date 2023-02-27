using Materal.BaseCore.Common;

namespace RC.ServerCenter.Web
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class WebAppConfig
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public static string AppName => MateralCoreConfig.GetValue(nameof(AppName), "MateralCoreApplication");
        /// <summary>
        /// 应用标题
        /// </summary>
        public static string AppTitle => MateralCoreConfig.GetValue(nameof(AppTitle), "MateralCore程序");
    }
}
