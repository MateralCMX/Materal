using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.Demo.Domain;
using RC.Demo.Domain.Repositories;
using RC.Demo.Enums;

namespace RC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public partial class UserRepositoryImpl: RCEFRepositoryImpl<User, Guid, DemoDBContext>, IUserRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserRepositoryImpl(DemoDBContext dbContext) : base(dbContext) { }
    }
}
