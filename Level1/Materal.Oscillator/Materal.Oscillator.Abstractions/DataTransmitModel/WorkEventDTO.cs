namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class WorkEventDTO
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
        /// 值
        /// </summary>
        public string Value { get; set; } = string.Empty;
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
    }
}
