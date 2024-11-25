using Materal.MergeBlock.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace MMB.Demo.Application.Oscillator
{
    public class MyWorkData() : MergeBlockWorkData("测试任务")
    {
        public override ICollection<IPlanTriggerData> GetPlanTriggers()
        {
            //RepeatPlanTriggerData result = new()
            //{
            //    DateTrigger = new DayDateTriggerData()
            //    {
            //        StartDate = DateTime.Now.ToDateOnly(),
            //        Interval = 1
            //    },
            //    TimeTrigger = new RepeatTimeTriggerData()
            //    {
            //        StartTime = new(0, 0, 0),
            //        EndTime = new(23, 59, 59),
            //        Interval = 5,
            //        IntervalType = TimeTriggerIntervalType.Second
            //    }
            //};
            OneTimePlanTriggerData result = new()
            {
                StartTime = DateTime.Now.AddDays(5)
            };
            return [result];
        }
    }
}
