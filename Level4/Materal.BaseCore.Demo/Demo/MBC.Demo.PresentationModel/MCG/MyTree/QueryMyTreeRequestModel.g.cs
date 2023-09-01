#nullable enable
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.PresentationModel.MyTree
{
    /// <summary>
    /// 我的树查询请求模型
    /// </summary>
    public partial class QueryMyTreeRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        public DateTime? MaxCreateTime { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        public DateTime? MinCreateTime { get; set; }
    }
}
