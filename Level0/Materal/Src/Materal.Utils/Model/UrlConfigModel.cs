namespace Materal.Utils.Model
{
    /// <summary>
    /// Url配置模型
    /// </summary>
    public class UrlConfigModel
    {
        /// <summary>
        /// 服务路径
        /// </summary>
        public string Url => $"{(IsSSL ? "https" : "http")}://{Host}:{Port}/{Path}";
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; } = "localhost";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 80;
        /// <summary>
        /// SSL
        /// </summary>
        public bool IsSSL { get; set; } = false;
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
    }
}
