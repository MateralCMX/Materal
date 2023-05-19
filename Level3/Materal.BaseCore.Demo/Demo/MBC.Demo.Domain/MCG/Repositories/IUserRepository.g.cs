using Materal.TTA.EFRepository;
using MBC.Core.Domain.Repositories;
using MBC.Demo.Enums;

namespace MBC.Demo.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IMBCRepository<User, Guid>
    {
    }
}
