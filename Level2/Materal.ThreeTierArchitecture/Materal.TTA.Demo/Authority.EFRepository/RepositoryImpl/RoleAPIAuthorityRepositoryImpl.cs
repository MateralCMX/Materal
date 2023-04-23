using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 角色API权限仓储
    /// </summary>
    public class RoleAPIAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<RoleAPIAuthority, Guid>, IRoleAPIAuthorityRepository
    {
        public RoleAPIAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
