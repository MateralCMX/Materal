using RC.Core.EFRepository;
using RC.Demo.Domain;
using RC.Demo.Domain.Repositories;

namespace RC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public partial class UserRepositoryImpl: RCEFRepositoryImpl<User, Guid>, IUserRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserRepositoryImpl(DemoDBContext dbContext) : base(dbContext) { }
    }
}
