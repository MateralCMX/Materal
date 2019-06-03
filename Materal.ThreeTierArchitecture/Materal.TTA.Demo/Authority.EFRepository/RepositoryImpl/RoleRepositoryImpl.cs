using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleRepositoryImpl : AuthorityEFRepositoryImpl<Role, Guid>, IRoleRepository
    {
        public RoleRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
