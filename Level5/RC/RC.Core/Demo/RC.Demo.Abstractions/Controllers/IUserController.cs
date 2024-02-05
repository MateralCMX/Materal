﻿using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.RequestModel.User;

namespace RC.Demo.Abstractions.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial interface IUserController
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id);
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        Task<ResultModel<UserDTO>> GetLoginUserInfoAsync();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
    }
}