using RC.Deploy.Application.Models;

namespace RC.Deploy.Application
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class ApplicationConfig
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = "RCDeploy";
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; } = "RC发布程序";
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string UploadFilePath { get; set; } = "UploadFiles";
        /// <summary>
        /// WinRar路径
        /// </summary>
        public string WinRarPath { get; set; } = "C:\\Program Files\\WinRAR";
        /// <summary>
        /// 控制台消息数量
        /// </summary>
        public int MaxConsoleMessageCount { get; set; } = 500;
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