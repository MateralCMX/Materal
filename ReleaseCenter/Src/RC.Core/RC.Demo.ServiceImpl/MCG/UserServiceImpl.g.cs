using Materal.BaseCore.ServiceImpl;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Domain;
using RC.Demo.Domain.Repositories;
using RC.Demo.Services;
using RC.Demo.Services.Models.User;

namespace RC.Demo.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User>, IUserService
    {
    }
}
