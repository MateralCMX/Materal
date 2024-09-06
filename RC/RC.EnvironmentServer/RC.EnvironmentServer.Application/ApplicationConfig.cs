namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    [Options("EnvironmentServer")]
    public class ApplicationConfig : IOptions
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = "RCEnvironmentServer";
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; } = "RC环境服务";
    }
}