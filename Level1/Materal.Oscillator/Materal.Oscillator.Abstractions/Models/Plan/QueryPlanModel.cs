using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.Plan
{
    public class QueryPlanModel : QueryPlanManagerModel
    {
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Contains]
        public string? ScheduleName { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        [Equal]
        public string? Territory { get; set; }
    }
}
