using Materal.BaseCore.ServiceImpl;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Domain;
using RC.Authority.Domain.Repositories;
using RC.Authority.Services;
using RC.Authority.Services.Models.User;

namespace RC.Authority.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User>, IUserService
    {
    }
}
