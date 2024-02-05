﻿namespace RC.Demo.Abstractions.Services.Models.User
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = string.Empty;
    }
}