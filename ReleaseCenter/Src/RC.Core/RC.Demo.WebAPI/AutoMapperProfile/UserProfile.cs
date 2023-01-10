using RC.Demo.PresentationModel;
using RC.Demo.PresentationModel.User;
using RC.Demo.Services.Models.User;

namespace RC.Demo.WebAPI.AutoMapperProfile
{
    /// <summary>
    /// 用户映射配置
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
