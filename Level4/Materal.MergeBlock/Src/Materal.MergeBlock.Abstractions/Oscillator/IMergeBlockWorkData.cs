﻿using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Abstractions.Oscillator
{
    /// <summary>
    /// MergeBlock任务数据
    /// </summary>
    public interface IMergeBlockWorkData : IWorkData
    {
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        ICollection<IPlanTrigger> GetPlanTriggers();
    }
}
