#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;

namespace MBC.Demo.DataTransmitModel.MyTree
{
    /// <summary>
    /// 我的树列表数据传输模型
    /// </summary>
    public partial class MyTreeListDTO: IListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间为空")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填"), StringLength(20, ErrorMessage = "名称长度必须小于等于20")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; } 
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; } 
    }
}
