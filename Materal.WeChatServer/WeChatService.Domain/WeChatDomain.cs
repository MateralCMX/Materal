using Domain;
using System;
namespace WeChatService.Domain
{
    /// <summary>
    /// 微信域名
    /// </summary>
    public sealed class WeChatDomain : BaseEntity<Guid>
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
    }
}
