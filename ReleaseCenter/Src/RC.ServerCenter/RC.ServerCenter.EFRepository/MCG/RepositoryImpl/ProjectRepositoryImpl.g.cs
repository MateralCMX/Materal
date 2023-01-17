using RC.Core.EFRepository;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;

namespace RC.ServerCenter.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 项目仓储实现
    /// </summary>
    public partial class ProjectRepositoryImpl: RCEFRepositoryImpl<Project, Guid>, IProjectRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProjectRepositoryImpl(ServerCenterDBContext dbContext) : base(dbContext) { }
    }
}
