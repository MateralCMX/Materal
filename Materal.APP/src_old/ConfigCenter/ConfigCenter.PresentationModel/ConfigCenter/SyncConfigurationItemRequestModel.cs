using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步配置项请求模型
    /// </summary>
    public class SyncConfigurationItemRequestModel
    {
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键不能为空"), StringLength(100, ErrorMessage = "键长度不能超过100")]
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
    }
}
