#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Services;

namespace MBC.Demo.Services.Models.MyTree
{
    /// <summary>
    /// 我的树修改模型
    /// </summary>
    public partial class EditMyTreeModel : IEditServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填"), StringLength(20, ErrorMessage = "名称长度必须小于等于20")]
        public string Name { get; set; }  = string.Empty;
    }
}
