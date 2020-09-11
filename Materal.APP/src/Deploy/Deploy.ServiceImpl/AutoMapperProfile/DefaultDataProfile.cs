using AutoMapper;
using Deploy.DataTransmitModel.DefaultData;
using Deploy.Domain;

namespace Deploy.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class DefaultDataProfile : Profile
    {
        public DefaultDataProfile()
        {
            CreateMap<DefaultData, DefaultDataListDTO>();
            CreateMap<DefaultData, DefaultDataDTO>();
        }
    }
}
