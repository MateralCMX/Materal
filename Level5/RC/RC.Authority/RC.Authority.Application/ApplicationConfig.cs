namespace RC.Authority.Application
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class ApplicationConfig
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public string DefaultPassword { get; set; } = "123456";
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string EncodePassword(string inputString) => $"Materal{inputString}Materal".ToMd5_32Encode();
    }
}
