using MBC.Demo.PresentationModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.WebAPI.AutoMapperProfile
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
