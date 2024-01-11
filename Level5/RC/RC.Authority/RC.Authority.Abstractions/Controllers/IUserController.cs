﻿using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.RequestModel.User;

namespace RC.Authority.Abstractions.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    public partial interface IUserController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel);
    }
}
