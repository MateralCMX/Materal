using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// MergeBlock任务数据
    /// </summary>
    public abstract class MergeBlockWorkData(string name) : WorkDataBase(name), IMergeBlockWorkData
    {
        /// <summary>
        /// 获取初始化计划触发器
        /// </summary>
        /// <returns></returns>
        public virtual IPlanTriggerData GetInitPlanTrigger() => new NowPlanTriggerData();
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        public abstract ICollection<IPlanTriggerData> GetPlanTriggers();
    }
}
