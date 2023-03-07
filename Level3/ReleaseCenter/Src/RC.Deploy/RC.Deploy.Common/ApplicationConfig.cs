using Materal.BaseCore.Common;
using RC.Deploy.Common.Models;

namespace RC.Deploy.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public partial class ApplicationConfig
    {
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public static string UploadFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MateralCoreConfig.GetValue(nameof(UploadFilePath), "UploadFiles"));
        /// <summary>
        /// WinRar路径
        /// </summary>
        public static string WinRarPath => MateralCoreConfig.GetValue(nameof(WinRarPath), string.Empty);
        /// <summary>
        /// 重写配置
        /// </summary>
        public static RewriteConfigModel RewriteConfig => MateralCoreConfig.GetValueObject<RewriteConfigModel>(nameof(RewriteConfig));
        /// <summary>
        /// 应用程序白名单
        /// </summary>
        public static string[] ApplicationNameWhiteList { get; } =
        {
            "api",
            "hubs",
            "swagger",
            "Deploy",
            "UploadFiles"
        };
    }
}
