using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 角色功能权限仓储
    /// </summary>
    public class RoleActionAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<RoleActionAuthority, Guid>, IRoleActionAuthorityRepository
    {
        public RoleActionAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
