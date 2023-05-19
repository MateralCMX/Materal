using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;

namespace RC.ServerCenter.Domain.Repositories
{
    /// <summary>
    /// 命名空间仓储接口
    /// </summary>
    public partial interface INamespaceRepository : IRCRepository<Namespace, Guid>
    {
    }
}
