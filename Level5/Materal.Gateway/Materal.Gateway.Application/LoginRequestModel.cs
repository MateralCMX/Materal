﻿namespace Materal.Gateway.Application
{
    /// <summary>
    /// 登录请求模型
    /// </summary>
    public class LoginRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码")]
        public string Password { get; set; } = string.Empty;
    }
}
