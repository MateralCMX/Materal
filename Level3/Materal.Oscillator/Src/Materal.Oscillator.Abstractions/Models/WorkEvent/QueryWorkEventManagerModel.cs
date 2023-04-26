using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.WorkEvent
{
    public class QueryWorkEventManagerModel : PageRequestModel, IScheduleIDModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [Equal]
        public string? Value { get; set; }
        /// <summary>
        /// 调度唯一标识
        /// </summary>
        [Equal]
        public Guid? ScheduleID { get; set; }
        /// <summary>
        /// 调度唯一标识
        /// </summary>
        [Contains(nameof(Domain.WorkEvent.ScheduleID))]
        public List<Guid>? ScheduleIDs { get; set; }
    }
}
