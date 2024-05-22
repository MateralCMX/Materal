namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务数据
    /// </summary>
    public interface IWorkData : IOscillatorData
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        string WorkTypeName { get; }
    }
}
