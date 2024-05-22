namespace Materal.Oscillator.PlanTriggers.TimeTrigger
{
    /// <summary>
    /// 时间不执行触发器
    /// </summary>
    public class TimeNotRunTrigger : TimeTriggerBase, ITimeTrigger
    {
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
        public override string GetDescriptionText() => $"的 任何时间点都不会执行。";
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetTriggerStartTime(Date date) => null;
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetTriggerEndTime(Date date) => null;
    }
}
