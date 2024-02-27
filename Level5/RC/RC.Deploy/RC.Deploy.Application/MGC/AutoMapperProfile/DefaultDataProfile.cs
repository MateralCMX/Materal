using RC.Deploy.Abstractions.Services.Models.DefaultData;
using RC.Deploy.Abstractions.RequestModel.DefaultData;
using RC.Deploy.Abstractions.DTO.DefaultData;

namespace RC.Deploy.Application.AutoMapperProfile
{
    /// <summary>
    /// 默认数据AutoMapper映射配置基类
    /// </summary>
    public partial class DefaultDataProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddDefaultDataModel, DefaultData>();
            CreateMap<AddDefaultDataRequestModel, AddDefaultDataModel>();
            CreateMap<EditDefaultDataModel, DefaultData>();
            CreateMap<EditDefaultDataRequestModel, EditDefaultDataModel>();
            CreateMap<QueryDefaultDataRequestModel, QueryDefaultDataModel>();
            CreateMap<DefaultData, DefaultDataListDTO>();
            CreateMap<DefaultData, DefaultDataDTO>();
        }
    }
    /// <summary>
    /// 默认数据AutoMapper映射配置
    /// </summary>
    public partial class DefaultDataProfile : DefaultDataProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultDataProfile()
        {
            Init();
        }
    }
}
