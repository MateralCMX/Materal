using AutoMapper;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.WebAPI.AutoMapperProfile
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
            CreateMap<EditUserModel, User>();
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<QueryUserRequestModel, QueryUserModel>();
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
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            base.Init();
            CreateMap<LoginRequestModel, LoginModel>();
            CreateMap<ChangePasswordRequestModel, ChangePasswordModel>();
        }
    }
}
