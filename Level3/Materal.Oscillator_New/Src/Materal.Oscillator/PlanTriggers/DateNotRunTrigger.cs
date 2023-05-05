using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
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
        public override DateTimeOffset? GetDateEndTime(IEveryDayTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateStartTime(IEveryDayTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override string GetDescriptionText(IEveryDayTrigger everyDayTrigger) => "不会执行";
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, IEveryDayTrigger everyDayTrigger) => null;
        /// <summary>
        /// 获得下次运行日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected override Date? GetNextRunDate(DateTimeOffset upRunTime) => null;
    }
}
