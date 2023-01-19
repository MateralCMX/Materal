namespace Common.Model
{
    /// <summary>
    /// 认证服务器配置模型
    /// </summary>
    public class IdentityServerConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 目标API
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 加密方式
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 文档地址
        /// </summary>
        public string DocumentUrl => $"{Url}/.well-known/openid-configuration";
    }
}
