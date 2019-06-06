using System;
using System.ComponentModel.DataAnnotations;
namespace WeChatService.PresentationModel.WeChatDomain.Request
{
    /// <summary>
    /// 微信域名修改请求模型
    /// </summary>
    public class EditWeChatDomainRequestModel : AddWeChatDomainRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
