using AutoMapper;
using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Domain;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 网页菜单权限AutoMapper配置
    /// </summary>
    public sealed class WebMenuAuthorityProfile : Profile
    {
        public WebMenuAuthorityProfile()
        {
            CreateMap<WebMenuAuthority, WebMenuAuthorityTreeDTO>();
            CreateMap<WebMenuAuthority, WebMenuAuthorityDTO>();
        }
    }
}
