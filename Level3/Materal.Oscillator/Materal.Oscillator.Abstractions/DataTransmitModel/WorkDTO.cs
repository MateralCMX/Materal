using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class WorkDTO
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
        /// 任务数据
        /// </summary>
        public IWork WorkData { get; set; } = new NoneWork();
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
