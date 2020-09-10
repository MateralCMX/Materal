using System.Collections.Generic;

namespace ConfigCenter.Environment.PresentationModel.ConfigurationItem
{
    /// <summary>
    /// 初始化配置项请求模型
    /// </summary>
    public class InitConfigurationItemsRequestModel
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public ICollection<AddConfigurationItemRequestModel> ConfigurationItems { get; set; }
    }
}
