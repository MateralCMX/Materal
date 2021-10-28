namespace Materal.APP.WebAPICore.Models
{
    public class ConsulServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        public string SocketPath { get; set; }
        public string EnableTagOverride { get; set; }
        public string Datacenter { get; set; }
    }
}
