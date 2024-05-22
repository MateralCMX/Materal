using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.Oscillator.PlanTriggers.DateTrigger
{
    /// <summary>
    /// 不运行触发器
    /// </summary>
    public class DateNotRunTrigger : DateTriggerBase, IDateTrigger
    {
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override string GetDescriptionText(ITimeTrigger everyDayTrigger) => "不会执行";
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得下次运行日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected override Date? GetNextRunDate(DateTimeOffset upRunTime) => null;
    }
}
