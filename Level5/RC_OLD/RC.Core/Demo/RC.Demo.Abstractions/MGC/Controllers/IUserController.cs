using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.RequestModel.User;

namespace RC.Demo.Abstractions.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial interface IUserController : IMergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
    }
}
