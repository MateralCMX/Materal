using System;
using WeChatService.Domain;
using WeChatService.Domain.Repositories;
namespace WeChatService.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 应用仓储
    /// </summary>
    public class ApplicationRepositoryImpl : WeChatServiceEFRepositoryImpl<Application, Guid>, IApplicationRepository
    {
        public ApplicationRepositoryImpl(WeChatServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
