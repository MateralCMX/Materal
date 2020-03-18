using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Namespace;
using Materal.ConfigCenter.ProtalServer.Domain;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl.AutoMapperProfile
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
