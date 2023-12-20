using Materal.MergeBlock.Application.Services;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Appllication.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserServiceImpl(IServiceProvider serviceProvider) : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User>(serviceProvider), IUserService
    {
    }
}
