using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 主机和端口号模型
    /// </summary>
    public class HostAndPortModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 主机
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Host { get; set; } = "localhost";
        /// <summary>
        /// 端口号
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int Port { get; set; } = 5000;
    }
}
