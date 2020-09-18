using Deploy.Enums;
using System.ComponentModel.DataAnnotations;

namespace Deploy.Services.Models.DefaultData
{
    /// <summary>
    /// 添加默认数据模型
    /// </summary>
    public class AddDefaultDataModel
    {
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键不能为空"), StringLength(100, ErrorMessage = "键长度不能超过100")]
        public string Key { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [Required(ErrorMessage = "数据不能为空")]
        public string Data { get; set; }
    }
}
