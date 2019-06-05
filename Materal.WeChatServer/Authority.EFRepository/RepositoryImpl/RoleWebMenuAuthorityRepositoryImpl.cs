using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 角色网页菜单权限仓储
    /// </summary>
    public class RoleWebMenuAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<RoleWebMenuAuthority, Guid>, IRoleWebMenuAuthorityRepository
    {
        public RoleWebMenuAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
