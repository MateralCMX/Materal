using Authority.DataTransmitModel.User;
using Authority.Domain;
using Authority.Services.Models.User;
using AutoMapper;

namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserModel, User>();
            CreateMap<EditUserModel, User>();
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
