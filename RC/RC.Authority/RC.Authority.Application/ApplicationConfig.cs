namespace RC.Authority.Application
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    [Options("Authority")]
    public class ApplicationConfig : IOptions
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public string DefaultPassword { get; set; } = "123456";
    }
}