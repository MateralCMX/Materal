using Microsoft.EntityFrameworkCore;
using MBC.Core.EFRepository;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;
using MBC.Demo.Enums;

namespace MBC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public partial class UserRepositoryImpl: MBCEFRepositoryImpl<User, Guid, DemoDBContext>, IUserRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserRepositoryImpl(DemoDBContext dbContext) : base(dbContext) { }
    }
}
