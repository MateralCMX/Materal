using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Services;

namespace MBC.Demo.Services.Models.MenuAuthority
{
    /// <summary>
    /// 修改模型
    /// </summary>
    public partial class EditMenuAuthorityModel : IEditServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称最多100个字符")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 代码
        /// </summary>
        [Required(ErrorMessage = "代码为空"), StringLength(100, ErrorMessage = "代码最多100个字符")]
        public string Code { get; set; }  = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(100, ErrorMessage = "地址最多100个字符")]
        public string? Path { get; set; } 
    }
}
