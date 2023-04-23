using AutoMapper;
using RC.Authority.PresentationModel.User;
using RC.Authority.Services.Models.User;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Domain;

namespace RC.Authority.WebAPI.AutoMapperProfile
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
    }
}
