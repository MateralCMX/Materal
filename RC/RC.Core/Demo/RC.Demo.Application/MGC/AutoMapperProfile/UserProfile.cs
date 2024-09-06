using RC.Demo.Abstractions.Services.Models.User;
using RC.Demo.Abstractions.RequestModel.User;
using RC.Demo.Abstractions.DTO.User;

/*
 * Generator Code From MateralMergeBlock=>GeneratorAutoMapperProfileAsync
 */
namespace RC.Demo.Application.AutoMapperProfile
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
