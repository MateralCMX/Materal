using MBC.Core.EFRepository;
using MBC.Demo.Domain;
using MBC.Demo.Domain.Repositories;

namespace MBC.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepositoryImpl : MBCEFRepositoryImpl<User, Guid>, IUserRepository
    {
    }
}
