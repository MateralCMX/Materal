using Materal.MergeBlock.Abstractions;
using Materal.Utils.Model;

namespace Materal.MergeBlock.Consul.Abstractions
{
    /// <summary>
    /// MergeBlockConsul配置
    /// </summary>
    public class MergeBlockConsulOptions : IOptions
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public static string ConfigKey { get; } = "Consul";
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; } = [];
        /// <summary>
        /// 地址
        /// </summary>
        public HttpUrlModel ConsulUrl { get; set; } = new()
        {
            IsSSL = false,
            Host = "127.0.0.1",
            Port = 8500
        };
        /// <summary>
        /// 服务地址
        /// </summary>
        public HttpUrlModel ServiceUrl { get; set; } = new()
        {
            IsSSL = false,
            Host = "localhost",
            Port = 5000
        };
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
        /// 服务名称后缀
        /// </summary>
        public string ServiceNameSuffix { get; set; } = "_DevAPI";
    }
}
