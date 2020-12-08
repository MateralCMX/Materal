namespace Materal.DotNetty.Client.Core
{
    public class ClientConfig
    {
        /// <summary>
        /// 是否是Wss
        /// </summary>
        public bool IsWss { get; set; } = false;
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public string Path{ get; set; }
    }
}
