namespace Materal.BaseCore.WebAPI.Common.Models
{
    /// <summary>
    /// Consul配置
    /// </summary>
    public class ConsulConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = "http://127.0.0.1:8500";
        /// <summary>
        /// 健康检查间隔
        /// </summary>
        public int HealthInterval { get; set; } = 10;
        /// <summary>
        /// 重连间隔
        /// </summary>
        public int ReconnectionInterval { get; set; } = 10;
    }
}
