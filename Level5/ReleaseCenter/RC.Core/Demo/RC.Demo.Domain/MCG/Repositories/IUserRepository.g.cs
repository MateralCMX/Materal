using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;
using RC.Demo.Enums;

namespace RC.Demo.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IRCRepository<User, Guid>
    {
    }
}
