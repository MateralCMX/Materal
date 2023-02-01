using Materal.TTA.EFRepository;

namespace MBC.Demo.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
