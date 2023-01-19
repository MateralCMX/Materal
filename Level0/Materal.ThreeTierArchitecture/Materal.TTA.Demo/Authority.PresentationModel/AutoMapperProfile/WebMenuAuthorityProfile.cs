using Authority.PresentationModel.WebMenuAuthority.Request;
using Authority.Service.Model.WebMenuAuthority;
using AutoMapper;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 网页菜单权限AutoMapper配置
    /// </summary>
    public sealed class WebMenuAuthorityProfile : Profile
    {
        /// <summary>
        /// 网页菜单权限AutoMapper配置
        /// </summary>
        public WebMenuAuthorityProfile()
        {
            CreateMap<AddWebMenuAuthorityRequestModel, AddWebMenuAuthorityModel>();
            CreateMap<EditWebMenuAuthorityRequestModel, EditWebMenuAuthorityModel>();
        }
    }
}
