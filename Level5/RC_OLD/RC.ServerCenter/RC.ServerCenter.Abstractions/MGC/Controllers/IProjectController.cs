using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.RequestModel.Project;

namespace RC.ServerCenter.Abstractions.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    public partial interface IProjectController : IMergeBlockControllerBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, ProjectDTO, ProjectListDTO>
    {
    }
}
