using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.Services.Models.Project;

namespace RC.ServerCenter.Application.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public partial class ProjectServiceImpl : BaseServiceImpl<AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO, IProjectRepository, Project, IServerCenterUnitOfWork>, IProjectService, IScopedDependencyInjectionService<IProjectService>
    {
    }
}
