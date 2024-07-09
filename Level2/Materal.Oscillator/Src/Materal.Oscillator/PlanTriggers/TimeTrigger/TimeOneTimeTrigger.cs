﻿namespace Materal.Oscillator.PlanTriggers.TimeTrigger
{
    /// <summary>
    /// 时间执行一次触发器
    /// </summary>
    public class TimeOneTimeTrigger : TimeTriggerBase, ITimeTrigger
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeOnly StartTime { get; set; } = new(0, 0, 0);
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => $"{StartTime} 执行一次。";
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetTriggerStartTime(DateOnly date) => date.MergeDateTimeOffset(StartTime);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetTriggerEndTime(DateOnly date) => null;
    }
}
