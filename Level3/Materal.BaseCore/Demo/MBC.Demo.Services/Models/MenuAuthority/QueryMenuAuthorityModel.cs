using Materal.Utils.Model;
using Materal.BaseCore.Services;

namespace MBC.Demo.Services.Models.MenuAuthority
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public partial class QueryMenuAuthorityModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Equal]
        public string? Code { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [Equal]
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        [Equal]
        public Guid? SubSystemID { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains("ID")]
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
    }
}
