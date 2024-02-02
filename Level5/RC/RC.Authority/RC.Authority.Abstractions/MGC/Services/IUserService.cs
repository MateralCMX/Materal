using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Abstractions.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
    }
}
