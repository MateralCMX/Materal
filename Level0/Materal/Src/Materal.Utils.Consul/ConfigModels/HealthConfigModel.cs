using Materal.Utils.Model;

namespace Materal.Utils.Consul.ConfigModels
{
    /// <summary>
    /// 健康检查配置模型
    /// </summary>
    public class HealthConfigModel
    {
        /// <summary>
        /// 健康检查间隔(秒)
        /// </summary>
        public int HealthInterval { get; set; } = 10;
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public HttpUrlModel HealthUrl { get; set; } = new()
        {
            IsSSL = false,
            Host = "localhost",
            Port = 5000,
            Path = "/api/Health"
        };
        /// <summary>
        /// 重连间隔(秒)
        /// </summary>
        public int ReconnectionInterval { get; set; } = 5;
    }
}
