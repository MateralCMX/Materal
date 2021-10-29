using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Environment.PresentationModel.ConfigurationItem
{
    /// <summary>
    /// 修改配置项请求模型
    /// </summary>
    public class EditConfigurationItemRequestModel : AddConfigurationItemRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
