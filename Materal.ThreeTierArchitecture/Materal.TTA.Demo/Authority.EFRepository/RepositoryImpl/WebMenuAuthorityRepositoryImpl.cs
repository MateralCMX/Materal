using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 网页菜单权限仓储
    /// </summary>
    public class WebMenuAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<WebMenuAuthority, Guid>, IWebMenuAuthorityRepository
    {
        public WebMenuAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
