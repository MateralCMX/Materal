using AutoMapper;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.Domain;

namespace ConfigCenter.Environment.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ConfigurationItemProfile : Profile
    {
        public ConfigurationItemProfile()
        {
            CreateMap<ConfigurationItem, ConfigurationItemListDTO>();
            CreateMap<ConfigurationItem, ConfigurationItemDTO>();
        }
    }
}
