using Materal.BaseCore.Domain;
using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using System.Linq.Expressions;
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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ProjectServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
