using Materal.Oscillator.Abstractions.Domain;
using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 查询任务模型
    /// </summary>
    public class QueryWorkModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains(nameof(Work.ID))]
        public List<Guid>? IDs { get; set; }
    }
}
