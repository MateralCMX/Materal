using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Model;
using System.ComponentModel.DataAnnotations;

namespace RC.Authority.Domain
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
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号最多100个字符")]
        [Equal]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [NotDTOGenerator, NotListDTOGenerator, NotAddGenerator, NotEditGenerator]
        [Required(ErrorMessage = "密码为空"), StringLength(100, ErrorMessage = "密码最多32个字符")]
        public string Password { get; set; } = string.Empty;
    }
}
