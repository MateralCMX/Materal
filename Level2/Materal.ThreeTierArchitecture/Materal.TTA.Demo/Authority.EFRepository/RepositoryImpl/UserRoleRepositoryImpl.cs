using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户角色仓储
    /// </summary>
    public class UserRoleRepositoryImpl : AuthorityEFRepositoryImpl<UserRole, Guid>, IUserRoleRepository
    {
        public UserRoleRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
