namespace Materal.Utils.Model
{
    /// <summary>
    /// HttpUrl模型
    /// </summary>
    public class HttpUrlModel
    {
        /// <summary>
        /// 服务路径
        /// </summary>
        public string Url
        {
            get
            {
                string result = $"{(IsSSL ? "https" : "http")}://{Host}:{Port}";
                if (Path is not null)
                {
                    if (Path[0] == '/')
                    {
                        result += Path;
                    }
                    else
                    {
                        result += $"/{Path}";
                    }
                }
                return result;
            }
        }

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
        /// <summary>
        /// 构造方法
        /// </summary>
        public HttpUrlModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="url"></param>
        public HttpUrlModel(string url)
        {
            Uri uri = new(url);
            Host = uri.Host;
            Port = uri.Port;
            IsSSL = uri.Scheme == "https";
            Path = uri.PathAndQuery;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="isSSL"></param>
        /// <param name="path"></param>
        public HttpUrlModel(string host, int port, bool isSSL, string? path = null)
        {
            Host = host;
            Port = port;
            IsSSL = isSSL;
            Path = path;
        }
    }
}
