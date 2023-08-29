using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;

namespace RC.Authority.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IRCRepository<User, Guid>
    {
    }
}
