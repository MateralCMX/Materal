using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.RequestModel.User;
using RC.Demo.Abstractions.Services.Models.User;

namespace RC.Demo.Application.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
    }
}
