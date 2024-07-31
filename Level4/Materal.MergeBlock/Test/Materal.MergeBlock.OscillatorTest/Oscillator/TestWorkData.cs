using Materal.Extensions;
using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.MergeBlock.OscillatorTest.Oscillator
{
    /// <summary>
    /// 测试任务数据
    /// </summary>
    public partial class TestWorkData() : MergeBlockWorkData("测试任务")
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "Hello World!";
        /// <summary>
        /// 获得初始化计划触发器
        /// </summary>
        /// <returns></returns>
        public override IPlanTriggerData GetInitPlanTrigger() => new OneTimePlanTriggerData() { StartTime = DateTime.Now.AddSeconds(10) };
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <returns></returns>
        public override ICollection<IPlanTriggerData> GetPlanTriggers()
        {
            IPlanTriggerData planTrigger = new RepeatPlanTriggerData
            {
                Name = "测试计划",
                Enable = true,
                DateTrigger = new DayDateTriggerData()
                {
                    StartDate = DateTime.Now.ToDateOnly(),
                    Interval = 1
                },
                TimeTrigger = new RepeatTimeTriggerData()
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
