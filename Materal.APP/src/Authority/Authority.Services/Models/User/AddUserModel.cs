﻿using System.ComponentModel.DataAnnotations;

namespace Authority.Services.Models.User
{
    public class AddUserModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空"), StringLength(100, ErrorMessage = "账号长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空"), StringLength(100, ErrorMessage = "姓名长度不能超过100")]
        public string Name { get; set; }
    }
}
