using AutoMapper;
using MMB.Demo.Abstractions.Domain;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Application.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper映射配置基类
    /// </summary>
    public partial class UserProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddUserModel, User>();
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserModel, User>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<QueryUserRequestModel, QueryUserModel>();
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
        }
    }
    /// <summary>
    /// 用户AutoMapper映射配置
    /// </summary>
    public partial class UserProfile : UserProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserProfile()
        {
            Init();
        }
    }
}
