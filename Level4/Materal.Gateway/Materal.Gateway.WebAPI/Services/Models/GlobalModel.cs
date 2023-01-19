namespace Materal.Gateway.WebAPI.Services.Models
{
    /// <summary>
    /// 全局模型
    /// </summary>
    public class GlobalModel
    {
        /// <summary>
        /// 根路径
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// 服务发现配置
        /// </summary>
        public ServiceDiscoveryModel ServiceDiscovery{ get; set; }
        /// <summary>
        /// 服务发现配置
        /// </summary>
        public class ServiceDiscoveryModel
        {
            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enable{ get; set; }
            /// <summary>
            /// 主机
            /// </summary>
            public string Host{ get; set; }
            /// <summary>
            /// 端口号
            /// </summary>
            public int Port{ get; set; }
            /// <summary>
            /// 类型
            /// </summary>
            public string Type{ get; set; }
        }

    }

}
