using Materal.BaseCore.Services;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Services.Models.User;

namespace RC.Authority.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
