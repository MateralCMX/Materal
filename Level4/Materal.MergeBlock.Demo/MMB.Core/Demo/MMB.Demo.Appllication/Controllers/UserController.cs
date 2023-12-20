using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Appllication.Controllers
{
    /// <summary>
    /// ���Կ�����
    /// </summary>
    public class UserController : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
    }
}
