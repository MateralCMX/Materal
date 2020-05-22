using System;

namespace Materal.Gateway.Models
{
    /// <summary>
    /// 配置项模型
    /// </summary>
    public class ConfigItemModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = "NewServiceName";

        /// <summary>
        /// 启用缓存
        /// </summary>
        public bool EnableCache { get; set; } = false;
    }
}
