using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.Services.Models.User;

namespace RC.Demo.Application.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User, IDemoUnitOfWork>, IUserService, IScopedDependencyInjectionService<IUserService>
    {
    }
}
