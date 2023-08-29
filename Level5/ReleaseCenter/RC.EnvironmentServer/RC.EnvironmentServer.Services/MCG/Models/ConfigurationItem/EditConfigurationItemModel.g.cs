#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Services;

namespace RC.EnvironmentServer.Services.Models.ConfigurationItem
{
    /// <summary>
    /// 配置项修改模型
    /// </summary>
    public partial class EditConfigurationItemModel : IEditServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键为空"), StringLength(50, ErrorMessage = "键过长")]
        public string Key { get; set; }  = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值为空")]
        public string Value { get; set; }  = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; }  = string.Empty;
    }
}
