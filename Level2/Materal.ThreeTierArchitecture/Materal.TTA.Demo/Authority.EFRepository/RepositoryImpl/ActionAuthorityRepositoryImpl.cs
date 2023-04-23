using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 功能权限仓储
    /// </summary>
    public class ActionAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<ActionAuthority, Guid>, IActionAuthorityRepository
    {
        public ActionAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
