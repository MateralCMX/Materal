using System;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
namespace WeChatService.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 微信域名仓储
    /// </summary>
    public class WeChatDomainRepositoryImpl : WeChatServiceEFRepositoryImpl<WeChatDomain, Guid>, IWeChatDomainRepository
    {
        public WeChatDomainRepositoryImpl(WeChatServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
