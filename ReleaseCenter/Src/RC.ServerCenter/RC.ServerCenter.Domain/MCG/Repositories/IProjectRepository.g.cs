using Materal.TTA.EFRepository;

namespace RC.ServerCenter.Domain.Repositories
{
    /// <summary>
    /// 项目仓储接口
    /// </summary>
    public partial interface IProjectRepository : IEFRepository<Project, Guid> { }
}
