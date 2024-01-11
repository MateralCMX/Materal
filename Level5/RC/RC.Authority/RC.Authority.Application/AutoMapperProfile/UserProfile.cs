using RC.Authority.Abstractions.RequestModel.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Abstractions.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper映射配置
    /// </summary>
    public partial class UserProfile
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
