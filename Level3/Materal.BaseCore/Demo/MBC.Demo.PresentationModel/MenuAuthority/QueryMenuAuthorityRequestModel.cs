using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;

namespace MBC.Demo.PresentationModel.MenuAuthority
{
    /// <summary>
    /// 查询请求模型
    /// </summary>
    public partial class QueryMenuAuthorityRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        public Guid? SubSystemID { get; set; }
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
