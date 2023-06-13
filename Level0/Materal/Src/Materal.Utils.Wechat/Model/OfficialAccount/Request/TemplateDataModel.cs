using System.ComponentModel.DataAnnotations;

namespace Materal.Utils.Wechat.Model.OfficialAccount.Request
{
    /// <summary>
    /// 模版数据模型
    /// </summary>
    public class TemplateDataModel
    {
        /// <summary>
        /// 模版键
        /// </summary>
        [Required(ErrorMessage = "模版键必填")]
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值必填")]
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 颜色
        /// </summary>
        [Required(ErrorMessage = "颜色必填")]
        public string Color { get; set; } = "#FF0000";
    }
}
