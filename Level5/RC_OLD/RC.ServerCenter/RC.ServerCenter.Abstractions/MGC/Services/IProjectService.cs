using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.Services.Models.Project;

namespace RC.ServerCenter.Abstractions.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public partial interface IProjectService : IBaseService<AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO>
    {
    }
}
