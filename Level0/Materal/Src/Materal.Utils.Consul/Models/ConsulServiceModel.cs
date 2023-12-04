namespace Materal.Utils.Consul.Models
{
    /// <summary>
    /// Consule服务模型
    /// </summary>
    public class ConsulServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string? ID { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public string? Service { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string[]? Tags { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Socket路径
        /// </summary>
        public string? SocketPath { get; set; }
        /// <summary>
        /// 启用标签重写
        /// </summary>
        public bool? EnableTagOverride { get; set; }
        /// <summary>
        /// 数据中心
        /// </summary>
        public string? Datacenter { get; set; }
    }
}
