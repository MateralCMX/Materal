using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;
using RC.ServerCenter.Services;
using RC.ServerCenter.Services.Models.Project;

namespace RC.ServerCenter.ServiceImpl
{
    /// <summary>
    /// 项目服务实现
    /// </summary>
    public partial class ProjectServiceImpl : BaseServiceImpl<AddProjectModel, EditProjectModel, QueryProjectModel, ProjectDTO, ProjectListDTO, IProjectRepository, Project>, IProjectService
    {
    }
}
