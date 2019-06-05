using Authority.DataTransmitModel.ActionAuthority;
using Authority.Domain;
using Authority.Domain.Views;
using AutoMapper;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 功能权限AutoMapper配置
    /// </summary>
    public sealed class ActionAuthorityProfile : Profile
    {
        public ActionAuthorityProfile()
        {
            CreateMap<UserOwnedActionAuthority, ActionAuthorityListDTO>();
            CreateMap<ActionAuthority, ActionAuthorityListDTO>();
            CreateMap<ActionAuthority, ActionAuthorityDTO>();
        }
    }
}
