using System;
using Authority.Domain.Views;
using Authority.Domain.Repositories.Views;
namespace Authority.EFRepository.RepositoryImpl.Views
{
    /// <summary>
    /// 用户拥有的API权限仓储
    /// </summary>
    public class UserOwnedAPIAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<UserOwnedAPIAuthority, Guid>, IUserOwnedAPIAuthorityRepository
    {
        public UserOwnedAPIAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
