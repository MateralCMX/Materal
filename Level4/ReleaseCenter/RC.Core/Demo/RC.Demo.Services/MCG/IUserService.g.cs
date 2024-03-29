using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Services.Models.User;

namespace RC.Demo.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
