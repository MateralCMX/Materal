using AutoMapper;
using RC.Deploy.PresentationModel.ApplicationInfo;
using RC.Deploy.Services.Models.ApplicationInfo;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Domain;

namespace RC.Deploy.WebAPI.AutoMapperProfile
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
            CreateMap<EditApplicationInfoModel, ApplicationInfo>();
            CreateMap<AddApplicationInfoRequestModel, AddApplicationInfoModel>();
            CreateMap<EditApplicationInfoRequestModel, EditApplicationInfoModel>();
            CreateMap<ApplicationInfo, ApplicationInfoListDTO>();
            CreateMap<ApplicationInfo, ApplicationInfoDTO>();
            CreateMap<QueryApplicationInfoRequestModel, QueryApplicationInfoModel>();
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
