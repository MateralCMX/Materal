using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Services.Models.User;

namespace RC.Authority.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
