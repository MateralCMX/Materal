using MMB.Core.Reposiroty;

namespace MMB.Demo.Domain.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IMMBRepository<User, Guid>
    {
    }
}
