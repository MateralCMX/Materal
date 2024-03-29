using AutoMapper;
using MBC.Demo.PresentationModel.User;
using MBC.Demo.Services.Models.User;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Domain;

namespace MBC.Demo.WebAPI.AutoMapperProfile
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
