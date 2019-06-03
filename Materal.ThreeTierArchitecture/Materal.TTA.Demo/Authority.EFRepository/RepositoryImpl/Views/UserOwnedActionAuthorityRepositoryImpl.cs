using System;
using Authority.Domain.Views;
using Authority.Domain.Repositories.Views;
namespace Authority.EFRepository.RepositoryImpl.Views
{
    /// <summary>
    /// 用户拥有的功能权限仓储
    /// </summary>
    public class UserOwnedActionAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<UserOwnedActionAuthority, Guid>, IUserOwnedActionAuthorityRepository
    {
        public UserOwnedActionAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
