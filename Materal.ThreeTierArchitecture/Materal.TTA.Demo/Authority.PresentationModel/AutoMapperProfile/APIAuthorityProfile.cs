using AutoMapper;
using Authority.PresentationModel.APIAuthority.Request;
using Authority.Service.Model.APIAuthority;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// API权限AutoMapper配置
    /// </summary>
    public sealed class APIAuthorityProfile : Profile
    {
        /// <summary>
        /// API权限AutoMapper配置
        /// </summary>
        public APIAuthorityProfile()
        {
            CreateMap<AddAPIAuthorityRequestModel, AddAPIAuthorityModel>();
            CreateMap<EditAPIAuthorityRequestModel, EditAPIAuthorityModel>();
        }
    }
}
