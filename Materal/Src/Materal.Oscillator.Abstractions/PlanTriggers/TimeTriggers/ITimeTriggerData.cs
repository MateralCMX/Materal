using System.Collections;

namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间触发器数据
    /// </summary>
    public interface ITimeTriggerData : IData
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
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        string GetDescriptionText();
    }
}
