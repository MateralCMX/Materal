using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.RequestModel.Project;
using RC.ServerCenter.Abstractions.Services.Models.Project;

namespace RC.ServerCenter.Application.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    public partial class ProjectController : MergeBlockControllerBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO, IProjectService>, IProjectController
    {
    }
}
