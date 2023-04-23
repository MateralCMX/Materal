using Materal.BaseCore.Domain;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.Domain
{
    public class MenuAuthority : BaseDomain, IDomain, ITreeDomain, IIndexDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称最多100个字符")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 代码
        /// </summary>
        [Equal]
        [Required(ErrorMessage = "代码为空"), StringLength(100, ErrorMessage = "代码最多100个字符")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(100, ErrorMessage = "地址最多100个字符")]
        public string? Path { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int Index { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [Equal]
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        [Equal]
        [Required(ErrorMessage = "子系统唯一标识为空")]
        public Guid SubSystemID { get; set; }
    }
}
