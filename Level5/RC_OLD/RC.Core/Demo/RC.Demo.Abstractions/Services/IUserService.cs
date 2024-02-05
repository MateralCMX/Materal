﻿using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.Services.Models.User;

namespace RC.Demo.Abstractions.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial interface IUserService
    {
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
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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