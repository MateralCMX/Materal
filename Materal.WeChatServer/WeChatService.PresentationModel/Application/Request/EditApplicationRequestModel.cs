using System;
using System.ComponentModel.DataAnnotations;
namespace WeChatService.PresentationModel.Application.Request
{
    /// <summary>
    /// 应用修改请求模型
    /// </summary>
    public class EditApplicationRequestModel : AddApplicationRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
