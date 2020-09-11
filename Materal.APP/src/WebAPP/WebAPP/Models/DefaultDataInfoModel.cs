using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models
{
    public class DefaultDataInfoModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(100, ErrorMessage = "不能超过100位")]
        public string Key { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        public string Data { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        public int ApplicationTypeValue { get; set; } = 0;
    }
}
