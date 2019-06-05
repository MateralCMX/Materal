using AutoMapper;
using Authority.PresentationModel.Role.Request;
using Authority.Service.Model.Role;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 角色AutoMapper配置
    /// </summary>
    public sealed class RoleProfile : Profile
    {
        /// <summary>
        /// 角色AutoMapper配置
        /// </summary>
        public RoleProfile()
        {
            CreateMap<AddRoleRequestModel, AddRoleModel>();
            CreateMap<EditRoleRequestModel, EditRoleModel>();
        }
    }
}
