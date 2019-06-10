using System.ComponentModel.DataAnnotations;

namespace WeChatService.PresentationModel.WeChatDomain.Request
{
    /// <summary>
    /// 微信域名添加请求模型
    /// </summary>
    public class AddWeChatDomainRequestModel
    {
        /// <summary>
        /// Url
        /// </summary>
        [Required(ErrorMessage = "Url不可以为空"), StringLength(100, ErrorMessage = "Url长度不能超过50")]
        public string Url { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不可以为空"), StringLength(100, ErrorMessage = "名称长度不能超过50")]
        public string Name { get; set; }
    }
}
