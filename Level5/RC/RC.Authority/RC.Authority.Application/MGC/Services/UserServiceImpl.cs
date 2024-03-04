/*
 * Generator Code From MateralMergeBlock=>GeneratorServiceImplsCode
 */
using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Application.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial class UserServiceImpl : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User, IAuthorityUnitOfWork>, IUserService, IScopedDependencyInjectionService<IUserService>
    {
    }
}
