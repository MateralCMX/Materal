using Materal.MergeBlock.Web.Abstractions.Controllers;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;

namespace MMB.Demo.Abstractions.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial interface IUserController : IMergeBlockController<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
    }
}
