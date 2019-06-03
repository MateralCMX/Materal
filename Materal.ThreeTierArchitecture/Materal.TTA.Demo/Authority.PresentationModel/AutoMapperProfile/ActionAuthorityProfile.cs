using AutoMapper;
using Authority.PresentationModel.ActionAuthority.Request;
using Authority.Service.Model.ActionAuthority;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 功能权限AutoMapper配置
    /// </summary>
    public sealed class ActionAuthorityProfile : Profile
    {
        /// <summary>
        /// 功能权限AutoMapper配置
        /// </summary>
        public ActionAuthorityProfile()
        {
            CreateMap<AddActionAuthorityRequestModel, AddActionAuthorityModel>();
            CreateMap<EditActionAuthorityRequestModel, EditActionAuthorityModel>();
            CreateMap<QueryActionAuthorityFilterRequestModel, QueryActionAuthorityFilterModel>();
        }
    }
}
