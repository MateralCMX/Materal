using MMB.Demo.Abstractions.Controllers;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController : DemoController<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
    }
}
