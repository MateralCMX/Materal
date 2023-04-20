﻿using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.Services.Models.User
{
    /// <summary>
    /// 更改密码模型
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
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
