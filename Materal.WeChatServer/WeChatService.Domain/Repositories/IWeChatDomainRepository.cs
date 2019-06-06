using Materal.TTA.Common;
using System;
namespace WeChatService.Domain.Repositories
{
    /// <summary>
    /// 微信域名仓储
    /// </summary>
    public interface IWeChatDomainRepository : IRepository<WeChatDomain, Guid>
    {
    }
}
