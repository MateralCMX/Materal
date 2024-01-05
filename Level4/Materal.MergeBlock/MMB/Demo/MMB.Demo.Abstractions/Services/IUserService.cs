using Materal.MergeBlock.Abstractions.Services;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Abstractions.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[MapperController(MapperType.Post)]
        Task<(List<UserListDTO> data, PageModel pageInfo)> GetUserListAsync(QueryUserModel model);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserDTO> LoginAsync(LoginModel model);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[MapperController(MapperType.Put)]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[MapperController(MapperType.Post)]
        Task TestChangePasswordAsync(ChangePasswordModel model);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(ChangePasswordModel model);
        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
        Task AddDefaultUserAsync();
    }
}
