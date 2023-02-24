using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.ScheduleWork
{
    public class QueryScheduleWorkModel : QueryScheduleWorkManagerModel
    {
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Contains]
        public string? ScheduleName { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Contains]
        public string? WorkName { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        [Equal]
        public string? Territory { get; set; }
    }
}
