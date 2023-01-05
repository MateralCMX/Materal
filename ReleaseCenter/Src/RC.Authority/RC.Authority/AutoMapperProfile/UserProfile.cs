using RC.Authority.PresentationModel.User;
using RC.Authority.Services.Models.User;

namespace RC.Authority.AutoMapperProfile
{
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
