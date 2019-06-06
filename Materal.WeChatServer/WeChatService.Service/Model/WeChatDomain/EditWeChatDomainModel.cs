using System;
namespace WeChatService.Service.Model.WeChatDomain
{
    /// <summary>
    /// 微信域名修改模型
    /// </summary>
    public class EditWeChatDomainModel : AddWeChatDomainModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
