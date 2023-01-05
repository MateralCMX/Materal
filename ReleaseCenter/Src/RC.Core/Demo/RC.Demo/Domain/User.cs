using Materal.Model;
using RC.CodeGenerator;
using RC.Core.Domain;
using RC.Demo.Enums;
using System.ComponentModel.DataAnnotations;

namespace RC.Demo.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDomain, IDomain
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名最多100个字符")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 测试时间
        /// </summary>
        [Required(ErrorMessage = "测试时间为空")]
        [Between]
        public DateTime TestTime { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别为空")]
        [Equal]
        public SexEnum Sex { get; set; }
    }
}
