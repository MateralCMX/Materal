using System;
using System.Collections.Generic;

namespace ConfigCenter.Environment.PresentationModel.ConfigurationItem
{
    /// <summary>
    /// 根据命名空间初始化配置项请求模型
    /// </summary>
    public class InitConfigurationItemsByNamespaceRequestModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public ICollection<AddConfigurationItemRequestModel> ConfigurationItems { get; set; }
    }
}
