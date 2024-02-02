using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.RequestModel.User;

namespace RC.Authority.Abstractions.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial interface IUserController : IMergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
    }
}
