using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using System.Collections;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 日期触发器数据
    /// </summary>
    public interface IDateTriggerData : IData
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
        /// 开始日期
        /// </summary>
        DateOnly StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        DateOnly? EndDate { get; set; }
        /// <summary>
        /// 间隔
        /// </summary>
        uint Interval { get; set; }
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="timeTriggerDate"></param>
        /// <returns></returns>
        string GetDescriptionText(ITimeTriggerData timeTriggerDate);
    }
}
