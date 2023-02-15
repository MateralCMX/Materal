namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 主机和端口号模型
    /// </summary>
    public class HostAndPortModel
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; } = "localhost";
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5000;
    }
}
