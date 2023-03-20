using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.Authority.Domain;
using RC.Authority.Domain.Repositories;

namespace RC.Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public partial class UserRepositoryImpl: RCEFRepositoryImpl<User, Guid>, IUserRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext) { }
    }
}
