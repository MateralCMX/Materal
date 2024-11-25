using Materal.MergeBlock.Application.Abstractions.Services;
using MMB.Demo.Abstractions;
using MMB.Demo.Abstractions.Domain;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Repositories;
using MMB.Demo.Abstractions.Services;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Application.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User, IDemoUnitOfWork>, IUserService, IScopedDependency<IUserService>
    {
    }
}
