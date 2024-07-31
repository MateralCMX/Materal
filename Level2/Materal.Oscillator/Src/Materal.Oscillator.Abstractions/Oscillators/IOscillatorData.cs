using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using System.Collections;

namespace Materal.Oscillator.Abstractions.Oscillators
{
    /// <summary>
    /// 调度器数据
    /// </summary>
    public interface IOscillatorData : IData
    {
        /// <summary>
        /// 映射表
        /// </summary>
        static Hashtable MapperTable { get; } = [];
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 作业数据
        /// </summary>
        IWorkData Work { get; set; }
        /// <summary>
        /// 触发器数据
        /// </summary>
        List<IPlanTriggerData> PlanTriggers { get; set; }
    }
}
