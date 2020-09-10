using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebAPP.Models
{
    public class UserInfoModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(100, ErrorMessage = "不能超过100位")]
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(100, ErrorMessage = "不能超过100位")]
        public string Name { get; set; }
    }
}
