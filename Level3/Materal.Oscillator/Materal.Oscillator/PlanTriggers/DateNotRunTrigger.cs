using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
{
    public class DateNotRunTrigger : DateTriggerBase, IDateTrigger
    {
        public override DateTimeOffset? GetDateEndTime(IEveryDayTrigger everyDayTrigger) => null;

        public override DateTimeOffset? GetDateStartTime(IEveryDayTrigger everyDayTrigger) => null;

        public override string GetDescriptionText(IEveryDayTrigger everyDayTrigger) => "不会执行";

        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, IEveryDayTrigger everyDayTrigger) => null;

        protected override Date? GetNextRunDate(DateTimeOffset upRunTime) => null;
    }
}
