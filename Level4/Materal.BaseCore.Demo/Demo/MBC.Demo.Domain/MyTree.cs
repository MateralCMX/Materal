using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.Domain
{
    /// <summary>
    /// 我的树
    /// </summary>
    [Cache]
    public class MyTree : BaseDomain, IDomain, ITreeDomain, IIndexDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填"), StringLength(20, ErrorMessage = "名称长度必须小于等于20")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        [Equal]
        [NotEditGenerator]
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        [NotAddGenerator, NotEditGenerator]
        public int Index { get; set; }
    }
}
