using AutoMapper;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ConfigurationItemProfile : Profile
    {
        public ConfigurationItemProfile()
        {
            CreateMap<ConfigurationItemListDTO, AddConfigurationItemModel>();
        }
    }
}
