using AutoMapper;
using Materal.ConDep.Center.DataTransmitModel.User;
using Materal.ConDep.Center.Domain;
using Materal.ConDep.Center.Services.Models.User;

namespace Materal.ConDep.Center.ServiceImpl.AutoMapperProfile
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
