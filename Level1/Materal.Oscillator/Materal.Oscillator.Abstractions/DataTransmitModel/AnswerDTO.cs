using Materal.Oscillator.Abstractions.Answers;

namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class AnswerDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 任务事件
        /// </summary>
        public string WorkEvent { get; set; } = string.Empty;
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 位序
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        public string ScheduleName { get; set; } = string.Empty;
        /// <summary>
        /// 响应数据
        /// </summary>
        public IAnswer? AnswerData { get; set; }
    }
}
