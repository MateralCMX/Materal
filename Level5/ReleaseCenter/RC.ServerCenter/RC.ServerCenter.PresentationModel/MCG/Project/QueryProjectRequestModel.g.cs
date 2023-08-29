#nullable enable
using Materal.Utils.Model;
using Materal.BaseCore.PresentationModel;
using System.ComponentModel.DataAnnotations;

namespace RC.ServerCenter.PresentationModel.Project
{
    /// <summary>
    /// 项目查询请求模型
    /// </summary>
    public partial class QueryProjectRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
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
