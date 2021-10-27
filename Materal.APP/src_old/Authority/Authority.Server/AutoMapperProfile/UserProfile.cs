using Authority.PresentationModel.User;
using Authority.Services.Models.User;
using AutoMapper;

namespace Authority.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public UserProfile()
        {
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<QueryUserFilterRequestModel, QueryUserFilterModel>();
            CreateMap<LoginRequestModel, LoginModel>();
            CreateMap<ChangePasswordRequestModel, ChangePasswordModel>();
        }
    }
}
