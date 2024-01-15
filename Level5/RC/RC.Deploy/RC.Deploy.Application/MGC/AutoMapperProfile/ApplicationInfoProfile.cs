using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.RequestModel.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models.ApplicationInfo;

namespace RC.Deploy.Abstraction.AutoMapperProfile
{
    /// <summary>
    /// 应用程序信息AutoMapper映射配置基类
    /// </summary>
    public partial class ApplicationInfoProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddApplicationInfoModel, ApplicationInfo>();
            CreateMap<AddApplicationInfoRequestModel, AddApplicationInfoModel>();
            CreateMap<EditApplicationInfoModel, ApplicationInfo>();
            CreateMap<EditApplicationInfoRequestModel, EditApplicationInfoModel>();
            CreateMap<QueryApplicationInfoRequestModel, QueryApplicationInfoModel>();
            CreateMap<ApplicationInfo, ApplicationInfoListDTO>();
            CreateMap<ApplicationInfo, ApplicationInfoDTO>();
        }
    }
    /// <summary>
    /// 应用程序信息AutoMapper映射配置
    /// </summary>
    public partial class ApplicationInfoProfile : ApplicationInfoProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApplicationInfoProfile()
        {
            Init();
        }
    }
}
