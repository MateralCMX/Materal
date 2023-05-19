using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;

namespace RC.ServerCenter.Domain.Repositories
{
    /// <summary>
    /// 项目仓储接口
    /// </summary>
    public partial interface IProjectRepository : IRCRepository<Project, Guid>
    {
    }
}
