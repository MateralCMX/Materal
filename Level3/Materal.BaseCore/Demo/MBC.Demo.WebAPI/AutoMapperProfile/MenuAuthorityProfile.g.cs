using AutoMapper;
using MBC.Demo.DataTransmitModel.MenuAuthority;
using MBC.Demo.Domain;
using MBC.Demo.PresentationModel.MenuAuthority;
using MBC.Demo.Services.Models.MenuAuthority;

namespace MBC.Demo.WebAPI.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public partial class MenuAuthorityProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MenuAuthorityProfile()
        {
            CreateMap<AddMenuAuthorityModel, MenuAuthority>();
            CreateMap<EditMenuAuthorityModel, MenuAuthority>();
            CreateMap<AddMenuAuthorityRequestModel, AddMenuAuthorityModel>();
            CreateMap<EditMenuAuthorityRequestModel, EditMenuAuthorityModel>();
            CreateMap<MenuAuthority, MenuAuthorityListDTO>();
            CreateMap<MenuAuthority, MenuAuthorityDTO>();
            CreateMap<QueryMenuAuthorityRequestModel, QueryMenuAuthorityModel>();
        }
    }
}
