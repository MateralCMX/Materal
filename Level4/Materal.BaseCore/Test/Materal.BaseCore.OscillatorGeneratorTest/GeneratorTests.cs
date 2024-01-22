using Materal.BaseCore.OscillatorGenerator;

namespace Materal.BaseCore.AutoDITest
{
    public class GeneratorTests
    {
        [Fact]
        public async Task ServiceImplDIGeneratorTest()
        {
            var source = @"using Materal.Abstractions;
using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Oscillator;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Services.Trigger;
using Materal.Oscillator.PlanTriggers;

namespace XMJ.Educational.Oscillator.Schedules
{
    [AutoWorkData]
    public class ReportedRemindSchedule : BaseOscillatorSchedule<ReportedRemindWorkData>, IOscillatorSchedule
    {
        public override string Name => ""上报提醒"";
        public override string? WorkDescription => ""每天提醒教师未上报的课程"";
        public override AddPlanModel GetPlan() => new()//每天晚上21点执行
        {
            Name = ""计划1"",
            PlanTriggerData = new RepeatPlanTrigger()
            {
                DateTrigger = new DateDayTrigger(),
                EveryDayTrigger = new EveryDayOneTimeTrigger() { StartTime = new Time(21, 0, 0) }
            }
        };
    }
}
";
            WorkDataGenerator generator = new();
            await TestHelper.Verify(source, generator);
        }
    }
}
