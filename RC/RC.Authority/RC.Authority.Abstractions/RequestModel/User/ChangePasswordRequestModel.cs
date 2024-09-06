﻿namespace RC.Authority.Abstractions.RequestModel.User
{
    /// <summary>
    /// 更改密码请求模型
    /// </summary>
    public class ChangePasswordRequestModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不能为空")]
        public string OldPassword { get; set; } = string.Empty;
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
