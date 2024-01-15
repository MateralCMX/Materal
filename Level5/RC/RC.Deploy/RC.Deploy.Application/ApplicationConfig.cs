using RC.Deploy.Application.Models;

namespace RC.Deploy.Application
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class ApplicationConfig
    {
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string UploadFilePath { get; set; } = "UploadFiles";
        /// <summary>
        /// WinRar路径
        /// </summary>
        public string WinRarPath { get; set; } = "C:\\Program Files\\WinRAR";
        /// <summary>
        /// 重写配置
        /// </summary>
        public RewriteConfigModel RewriteConfig { get; set; } = new();
        /// <summary>
        /// 应用程序白名单
        /// </summary>
        public static string[] ApplicationNameWhiteList { get; } =
        [
            "api",
            "hubs",
            "swagger",
            "Deploy",
            "UploadFiles"
        ];
    }
}
