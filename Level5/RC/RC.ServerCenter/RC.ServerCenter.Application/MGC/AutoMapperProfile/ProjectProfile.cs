using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.RequestModel.Project;
using RC.ServerCenter.Abstractions.Services.Models.Project;

namespace RC.ServerCenter.Abstractions.AutoMapperProfile
{
    /// <summary>
    /// 项目AutoMapper映射配置基类
    /// </summary>
    public partial class ProjectProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddProjectModel, Project>();
            CreateMap<AddProjectRequestModel, AddProjectModel>();
            CreateMap<EditProjectModel, Project>();
            CreateMap<EditProjectRequestModel, EditProjectModel>();
            CreateMap<QueryProjectRequestModel, QueryProjectModel>();
            CreateMap<Project, ProjectListDTO>();
            CreateMap<Project, ProjectDTO>();
        }
    }
    /// <summary>
    /// 项目AutoMapper映射配置
    /// </summary>
    public partial class ProjectProfile : ProjectProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProjectProfile()
        {
            Init();
        }
    }
}
