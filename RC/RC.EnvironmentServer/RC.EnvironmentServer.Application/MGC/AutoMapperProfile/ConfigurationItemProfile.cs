using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;

/*
 * Generator Code From MateralMergeBlock=>GeneratorAutoMapperProfileAsync
 */
namespace RC.EnvironmentServer.Application.AutoMapperProfile
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
            CreateMap<AddConfigurationItemRequestModel, AddConfigurationItemModel>();
            CreateMap<EditConfigurationItemModel, ConfigurationItem>();
            CreateMap<EditConfigurationItemRequestModel, EditConfigurationItemModel>();
            CreateMap<QueryConfigurationItemRequestModel, QueryConfigurationItemModel>();
            CreateMap<ConfigurationItem, ConfigurationItemListDTO>();
            CreateMap<ConfigurationItem, ConfigurationItemDTO>();
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
