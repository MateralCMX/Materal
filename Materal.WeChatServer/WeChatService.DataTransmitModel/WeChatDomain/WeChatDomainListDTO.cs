using System;
namespace WeChatService.DataTransmitModel.WeChatDomain
{
    /// <summary>
    /// 微信域名列表数据传输模型
    /// </summary>
    public class WeChatDomainListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
