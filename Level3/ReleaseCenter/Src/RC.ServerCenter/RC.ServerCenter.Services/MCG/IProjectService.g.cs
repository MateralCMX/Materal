using Materal.BaseCore.Services;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.Services.Models.Project;

namespace RC.ServerCenter.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IProjectService : IBaseService<AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO>
    {
    }
}
