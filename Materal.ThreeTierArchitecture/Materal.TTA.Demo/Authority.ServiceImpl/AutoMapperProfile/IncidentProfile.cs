using AutoMapper;
using Authority.DataTransmitModel.Incident;
using Authority.Domain;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 事件AutoMapper配置
    /// </summary>
    public sealed class IncidentProfile : Profile
    {
        public IncidentProfile()
        {
            CreateMap<Incident, IncidentListDTO>();
            CreateMap<Incident, IncidentDTO>();
        }
    }
}
