using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;
using RC.Deploy.Enums;

namespace RC.Deploy.Domain.Repositories
{
    /// <summary>
    /// 默认数据仓储接口
    /// </summary>
    public partial interface IDefaultDataRepository : IRCRepository<DefaultData, Guid>
    {
    }
}
