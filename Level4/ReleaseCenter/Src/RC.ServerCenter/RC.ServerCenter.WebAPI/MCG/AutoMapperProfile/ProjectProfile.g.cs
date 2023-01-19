using AutoMapper;
using RC.ServerCenter.PresentationModel.Project;
using RC.ServerCenter.Services.Models.Project;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.Domain;

namespace RC.ServerCenter.WebAPI.AutoMapperProfile
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
            CreateMap<EditProjectModel, Project>();
            CreateMap<AddProjectRequestModel, AddProjectModel>();
            CreateMap<EditProjectRequestModel, EditProjectModel>();
            CreateMap<Project, ProjectListDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<QueryProjectRequestModel, QueryProjectModel>();
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
