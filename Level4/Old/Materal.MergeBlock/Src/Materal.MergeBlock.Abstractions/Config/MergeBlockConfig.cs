namespace Materal.MergeBlock.Abstractions.Config
{
    /// <summary>
    /// 应用程序配置模型
    /// </summary>
    public class MergeBlockConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;
        /// <summary>
        /// Http客户端基础地址
        /// </summary>
        public string BaseUrl { get; set; } = "http://127.0.0.1:5000";
        /// <summary>
        /// 获得应用程序名称
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetApplicationName(IConfiguration configuration)
        {
            string applicationName = configuration.GetValue(nameof(ApplicationName)) ?? "MergeBlockApp";
            return applicationName;
        }
    }
}
