using AutoMapper;
using Materal.ConDep.Center.PresentationModel.User;
using Materal.ConDep.Center.Services.Models.User;

namespace Materal.ConDep.Center.Controllers.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<ChangePasswordRequestModel, ChangePasswordModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<LoginRequestModel, LoginModel>();
            CreateMap<QueryUserFilterRequestModel, QueryUserFilterModel>();
        }
    }
}
