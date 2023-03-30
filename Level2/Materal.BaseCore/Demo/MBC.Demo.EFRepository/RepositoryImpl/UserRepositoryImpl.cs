using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;

namespace MBC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepositoryImpl : DemoRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(DemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
