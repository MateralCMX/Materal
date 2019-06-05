using AutoMapper;
using Authority.DataTransmitModel.APIAuthority;
using Authority.Domain;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// API权限AutoMapper配置
    /// </summary>
    public sealed class APIAuthorityProfile : Profile
    {
        public APIAuthorityProfile()
        {
            CreateMap<APIAuthority, APIAuthorityTreeDTO>();
            CreateMap<APIAuthority, APIAuthorityDTO>();
        }
    }
}
