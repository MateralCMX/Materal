using AutoMapper;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;

namespace ConfigCenter.Environment.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ConfigurationItemProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public ConfigurationItemProfile()
        {
            CreateMap<AddConfigurationItemRequestModel, AddConfigurationItemModel>();
            CreateMap<EditConfigurationItemRequestModel, EditConfigurationItemModel>();
            CreateMap<QueryConfigurationItemFilterRequestModel, QueryConfigurationItemFilterModel>();
        }
    }
}
