using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Works;

namespace Materal.MergeBlock.Abstractions.Oscillator
{
    /// <summary>
    /// MergeBlock任务数据
    /// </summary>
    /// <typeparam name="TWork"></typeparam>
    public abstract class MergeBlockWorkData<TWork>(string name) : WorkData<TWork>(name), IMergeBlockWorkData
        where TWork : IWork
    {
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        public abstract ICollection<IPlanTrigger> GetPlanTriggers();
    }
}
