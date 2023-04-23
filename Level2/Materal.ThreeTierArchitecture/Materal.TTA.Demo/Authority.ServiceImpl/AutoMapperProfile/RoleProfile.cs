using AutoMapper;
using Authority.DataTransmitModel.Role;
using Authority.Domain;
namespace Authority.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 角色AutoMapper配置
    /// </summary>
    public sealed class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleTreeDTO>();
            CreateMap<Role, RoleDTO>();
        }
    }
}
