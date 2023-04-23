namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class ScheduleDTO
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
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
