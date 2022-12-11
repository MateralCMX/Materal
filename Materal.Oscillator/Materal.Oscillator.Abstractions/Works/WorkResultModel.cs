using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    public class WorkResultModel
    {
        /// <summary>
        /// 任务
        /// </summary>
        public ScheduleWorkView ScheduleWork { get; set; } = new ScheduleWorkView();
        /// <summary>
        /// 结果
        /// </summary>
        public string? Result { get; set; }
    }
}
