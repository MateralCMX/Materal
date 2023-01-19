using Materal.Model;

namespace Materal.Oscillator.Abstractions.Models.Answer
{
    public class QueryAnswerManagerModel : PageRequestModel, IScheduleIDModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Equal]
        public Guid? ScheduleID { get; set; }
        /// <summary>
        /// 任务事件
        /// </summary>
        [Equal]
        public string? WorkEvent { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        [Equal]
        public bool? Enable { get; set; }
        /// <summary>
        /// 调度器唯一标识组
        /// </summary>
        [Contains]
        public Guid[]? ScheduleIDs { get; set; }
    }
}
