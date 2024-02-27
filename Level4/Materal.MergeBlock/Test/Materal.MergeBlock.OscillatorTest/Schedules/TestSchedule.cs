using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.MergeBlock.OscillatorTest.WorkData;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.PlanTriggers;

namespace Materal.MergeBlock.OscillatorTest.Schedules
{
    /// <summary>
    /// 测试计划
    /// </summary>
    public class TestSchedule : MergeBlockSchedule<TestWorkData>, IOscillatorSchedule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "测试计划";
        /// <summary>
        /// 获得计划
        /// </summary>
        /// <returns></returns>
        public override AddPlanModel GetPlan() => new()//每天晚上22点执行
        {
            Name = "计划1",
            PlanTriggerData = new RepeatPlanTrigger()
            {
                DateTrigger = new DateDayTrigger(),
                EveryDayTrigger = new EveryDayRepeatTrigger() { Interval = 10, IntervalType = EveryDayIntervalType.Second }
            }
        };
    }
}
