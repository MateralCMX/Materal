using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepositoryImpl : AuthorityEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
