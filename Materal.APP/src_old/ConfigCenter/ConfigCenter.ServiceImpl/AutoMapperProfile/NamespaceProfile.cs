using AutoMapper;
using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.Domain;

namespace ConfigCenter.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class NamespaceProfile : Profile
    {
        public NamespaceProfile()
        {
            CreateMap<Namespace, NamespaceListDTO>();
            CreateMap<Namespace, NamespaceDTO>();
        }
    }
}
