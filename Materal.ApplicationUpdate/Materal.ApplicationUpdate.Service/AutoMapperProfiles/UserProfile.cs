using AutoMapper;
using Materal.ApplicationUpdate.Domain;
using Materal.ApplicationUpdate.DTO.User;

namespace Materal.ApplicationUpdate.Service.AutoMapperProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, LoginUserDTO>();
        }
    }
}
