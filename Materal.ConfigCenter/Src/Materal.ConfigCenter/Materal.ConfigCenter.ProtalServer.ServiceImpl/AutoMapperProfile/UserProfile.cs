using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.User;
using Materal.ConfigCenter.ProtalServer.Domain;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
