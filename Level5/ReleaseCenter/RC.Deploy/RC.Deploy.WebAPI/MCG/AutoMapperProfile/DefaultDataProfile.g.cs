using AutoMapper;
using RC.Deploy.PresentationModel.DefaultData;
using RC.Deploy.Services.Models.DefaultData;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.Domain;

namespace RC.Deploy.WebAPI.AutoMapperProfile
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
            CreateMap<EditDefaultDataModel, DefaultData>();
            CreateMap<AddDefaultDataRequestModel, AddDefaultDataModel>();
            CreateMap<EditDefaultDataRequestModel, EditDefaultDataModel>();
            CreateMap<DefaultData, DefaultDataListDTO>();
            CreateMap<DefaultData, DefaultDataDTO>();
            CreateMap<QueryDefaultDataRequestModel, QueryDefaultDataModel>();
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
