using System;
using System.Collections.Generic;

namespace Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem
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
        public List<AddConfigurationItemModel> ConfigurationItems { get; set; }
    }
}
