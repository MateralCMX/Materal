using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Abstractions.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public interface IUserController : IMergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>
    {
    }
}
