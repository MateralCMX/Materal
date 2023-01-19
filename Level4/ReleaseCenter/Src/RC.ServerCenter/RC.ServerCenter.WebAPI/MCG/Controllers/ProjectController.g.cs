using Materal.BaseCore.WebAPI.Controllers;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.PresentationModel.Project;
using RC.ServerCenter.Services;
using RC.ServerCenter.Services.Models.Project;

namespace RC.ServerCenter.WebAPI.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    public partial class ProjectController : MateralCoreWebAPIServiceControllerBase<AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO, IProjectService>
    {
    }
}
