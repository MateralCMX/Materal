using AutoMapper;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;

namespace RC.EnvironmentServer.WebAPI.AutoMapperProfile
{
    /// <summary>
    /// 配置项AutoMapper映射配置基类
    /// </summary>
    public partial class ConfigurationItemProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddConfigurationItemModel, ConfigurationItem>();
            CreateMap<EditConfigurationItemModel, ConfigurationItem>();
            CreateMap<AddConfigurationItemRequestModel, AddConfigurationItemModel>();
            CreateMap<EditConfigurationItemRequestModel, EditConfigurationItemModel>();
            CreateMap<ConfigurationItem, ConfigurationItemListDTO>();
            CreateMap<ConfigurationItem, ConfigurationItemDTO>();
            CreateMap<QueryConfigurationItemRequestModel, QueryConfigurationItemModel>();
        }
    }
    /// <summary>
    /// 配置项AutoMapper映射配置
    /// </summary>
    public partial class ConfigurationItemProfile : ConfigurationItemProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConfigurationItemProfile()
        {
            Init();
        }
    }
}
