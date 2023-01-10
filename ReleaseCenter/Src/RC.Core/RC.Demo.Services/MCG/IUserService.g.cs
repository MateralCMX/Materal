using Materal.BaseCore.Services;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Services.Models.User;

namespace RC.Demo.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
