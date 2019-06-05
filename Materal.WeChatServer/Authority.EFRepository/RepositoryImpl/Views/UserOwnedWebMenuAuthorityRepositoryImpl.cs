using System;
using Authority.Domain.Views;
using Authority.Domain.Repositories.Views;
namespace Authority.EFRepository.RepositoryImpl.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限仓储
    /// </summary>
    public class UserOwnedWebMenuAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<UserOwnedWebMenuAuthority, Guid>, IUserOwnedWebMenuAuthorityRepository
    {
        public UserOwnedWebMenuAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
