using RC.Demo.Abstractions.RequestModel.User;
using RC.Demo.Abstractions.Services.Models.User;

namespace RC.Demo.Application.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper映射配置
    /// </summary>
    public partial class UserProfile : UserProfileBase
    {
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