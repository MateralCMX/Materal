using Materal.Model;

namespace Materal.Oscillator.Abstractions.Models.ScheduleWork
{
    public class QueryScheduleWorkManagerModel : PageRequestModel, IScheduleIDModel
    {
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Equal]
        public Guid? ScheduleID { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        [Equal]
        public Guid? WorkID { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        [Equal]
        public string? SuccessEvent { get; set; }
        /// <summary>
        /// 失败事件
        /// </summary>
        [Equal]
        public string? FailEvent { get; set; }
        /// <summary>
        /// 调度器唯一标识组
        /// </summary>
        [Contains]
        public Guid[]? ScheduleIDs { get; set; }
    }
}
