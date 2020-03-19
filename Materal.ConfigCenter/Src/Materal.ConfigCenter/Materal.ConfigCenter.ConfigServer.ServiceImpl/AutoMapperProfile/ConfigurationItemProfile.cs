using AutoMapper;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.Domain;

namespace Materal.ConfigCenter.ConfigServer.ServiceImpl.AutoMapperProfile
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
