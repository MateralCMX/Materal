using Materal.MergeBlock.Domain.Abstractions;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace MMB.Demo.Abstractions.Domain
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
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(50, ErrorMessage = "账号过长")]
        [Equal]
        public string Account { get; set; } = string.Empty;
    }
}