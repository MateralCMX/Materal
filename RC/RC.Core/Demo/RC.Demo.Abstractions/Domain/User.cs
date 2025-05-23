﻿namespace RC.Demo.Abstractions.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDomain, IDomain
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [DTOText]
        [Required(ErrorMessage = "性别为空")]
        [Equal]
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(50, ErrorMessage = "账号过长")]
        [Equal]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [NotAdd, NotEdit, NotListDTO, NotDTO]
        [Required(ErrorMessage = "账号为空"), StringLength(32, ErrorMessage = "密码过长")]
        public string Password { get; set; } = string.Empty;
    }
}