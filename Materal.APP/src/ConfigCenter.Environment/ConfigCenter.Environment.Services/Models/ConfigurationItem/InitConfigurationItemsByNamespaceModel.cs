using System;
using System.Collections.Generic;

namespace ConfigCenter.Environment.Services.Models.ConfigurationItem
{
    public class InitConfigurationItemsByNamespaceModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public ICollection<AddConfigurationItemModel> ConfigurationItems { get; set; }
    }
}
