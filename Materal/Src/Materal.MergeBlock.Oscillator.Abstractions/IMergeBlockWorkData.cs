using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// MergeBlock任务数据
    /// </summary>
    public interface IMergeBlockWorkData : IWorkData
    {
        /// <summary>
        /// 获取初始化计划触发器
        /// </summary>
        /// <returns></returns>
        IPlanTriggerData GetInitPlanTrigger();
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        ICollection<IPlanTriggerData> GetPlanTriggers();
    }
}
