using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class ScheduleWorkDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        public string SuccessEvent { get; set; } = string.Empty;
        /// <summary>
        /// 失败事件
        /// </summary>
        public string FailEvent { get; set; } = string.Empty;
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        public string ScheduleName { get; set; } = string.Empty;
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        public Guid WorkID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string WorkName { get; set; } = string.Empty;
        /// <summary>
        /// 任务数据
        /// </summary>
        public IWork WorkData { get; set; } = new NoneWork();
    }
}
