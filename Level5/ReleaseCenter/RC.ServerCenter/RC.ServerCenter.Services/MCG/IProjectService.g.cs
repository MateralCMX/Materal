using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.Services.Models.Project;

namespace RC.ServerCenter.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public partial interface IProjectService : IBaseService<AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO>
    {
    }
}
