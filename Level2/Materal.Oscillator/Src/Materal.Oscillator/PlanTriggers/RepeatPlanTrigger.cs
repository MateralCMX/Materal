using Materal.Oscillator.Abstractions.Extensions;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 重复执行计划触发器
    /// </summary>
    public class RepeatPlanTrigger(IServiceProvider serviceProvider) : PlanTriggerBase<RepeatPlanTriggerData>, IPlanTrigger
    {
        private IDateTrigger? _dateTrigger;
        private ITimeTrigger? _timeTrigger;
        /// <inheritdoc/>
        protected override async Task InitAsync()
        {
            _dateTrigger = await Data.DateTrigger.GetDateTriggerAsync(serviceProvider);
            _timeTrigger = await Data.TimeTrigger.GetTimeTriggerAsync(serviceProvider);
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            if (_dateTrigger is null || _timeTrigger is null) return null;
            DateTimeOffset? startTime = _dateTrigger.GetDateStartTime(_timeTrigger);
            if (startTime is null) return null;
            DateTimeOffset? endTime = _dateTrigger.GetDateEndTime(_timeTrigger);
            ITrigger? trigger = CreateTrigger(triggerKey, startTime.Value.DateTime, endTime?.DateTime);
            return trigger;
        }
        /// <summary>
        /// 获得下一次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            if (_dateTrigger is null || _timeTrigger is null) return null;
            return _dateTrigger.GetNextRunTime(upRunTime, _timeTrigger);
        }
    }
}
