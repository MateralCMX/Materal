using AutoMapper;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Domain;
using MBC.Demo.PresentationModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.WebAPI.AutoMapperProfile
{
    /// <summary>
    /// 用户映射配置
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserProfile()
        {
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<QueryUserRequestModel, QueryUserModel>();
            CreateMap<AddUserModel, User>();
            CreateMap<EditUserModel, User>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserListDTO>();
            CreateMap<LoginRequestModel, LoginModel>();
            CreateMap<ChangePasswordRequestModel, ChangePasswordModel>();
        }
    }
}
