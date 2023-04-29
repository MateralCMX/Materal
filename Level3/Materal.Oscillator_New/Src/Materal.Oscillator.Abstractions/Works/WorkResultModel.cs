using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务返回模型
    /// </summary>
    public class WorkResultModel
    {
        /// <summary>
        /// 任务
        /// </summary>
        public ScheduleWork ScheduleWork { get; set; } = new ScheduleWork();
        /// <summary>
        /// 结果
        /// </summary>
        public string? Result { get; set; }
    }
}
