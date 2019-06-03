using AutoMapper;
using Authority.DataTransmitModel.ActionAuthority;
using Authority.Domain;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 功能权限AutoMapper配置
    /// </summary>
    public sealed class ActionAuthorityProfile : Profile
    {
        public ActionAuthorityProfile()
        {
            CreateMap<ActionAuthority, ActionAuthorityListDTO>();
            CreateMap<ActionAuthority, ActionAuthorityDTO>();
        }
    }
}
