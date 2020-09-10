using System.ComponentModel.DataAnnotations;

namespace Materal.APP.PresentationModel.StringHelper
{
    /// <summary>
    /// 字符串请求模型
    /// </summary>
    public class StringRequestModel
    {
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值不能为空")]
        public string Value { get; set; }
    }
}
