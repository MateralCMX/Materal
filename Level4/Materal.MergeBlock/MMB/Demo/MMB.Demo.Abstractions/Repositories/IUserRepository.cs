using MMB.Core.Reposiroty;
using MMB.Demo.Abstractions.Domain;

namespace MMB.Demo.Abstractions.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public partial interface IUserRepository : IMMBRepository<User, Guid>
    {
    }
}
