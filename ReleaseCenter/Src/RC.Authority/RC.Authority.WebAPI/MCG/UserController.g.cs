using Materal.BaseCore.WebAPI.Controllers;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.PresentationModel.User;
using RC.Authority.Services;
using RC.Authority.Services.Models.User;

namespace RC.Authority.WebAPI.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController : MateralCoreWebAPIServiceControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>
    {
    }
}
