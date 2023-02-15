using Materal.BaseCore.WebAPI.Controllers;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.PresentationModel.User;
using RC.Demo.Services;
using RC.Demo.Services.Models.User;

namespace RC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController : MateralCoreWebAPIServiceControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>
    {
    }
}