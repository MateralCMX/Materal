using Materal.Extensions;
using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.PlanTriggers.DateTrigger;
using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.MergeBlock.OscillatorTest.Oscillator
{
    /// <summary>
    /// 测试任务数据
    /// </summary>
    public partial class TestWorkData() : MergeBlockWorkData<TestWork>("测试任务")
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "Hello World!";
        /// <summary>
        /// 获得初始化计划触发器
        /// </summary>
        /// <returns></returns>
        public override IPlanTrigger GetInitPlanTrigger() => new OneTimePlanTrigger() { StartTime = DateTime.Now.AddSeconds(10) };
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        public override ICollection<IPlanTrigger> GetPlanTriggers()
        {
            IPlanTrigger planTrigger = new RepeatPlanTrigger
            {
                Name = "测试计划",
                Enable = true,
                DateTrigger = new DateDayTrigger()
                {
                    StartDate = DateTime.Now.ToDateOnly(),
                    Interval = 1
                },
                TimeTrigger = new TimeRepeatTrigger()
                {
                    StartTime = new(0, 0, 0),
                    EndTime = new(23, 59, 59),
                    Interval = 5,
                    IntervalType = TimeTriggerIntervalType.Second
                }
            };
            return [planTrigger];
        }
    }
}
