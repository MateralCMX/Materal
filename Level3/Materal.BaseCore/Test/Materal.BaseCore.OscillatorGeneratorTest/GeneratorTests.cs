using Materal.BaseCore.OscillatorGenerator;

namespace Materal.BaseCore.AutoDITest
{
    [UsesVerify]
    public class GeneratorTests
    {
        [Fact]
        public async Task ServiceImplDIGeneratorTest()
        {
            var source = @"using Materal.Abstractions;
using Materal.BaseCore.Oscillator;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Services.Trigger;
using Materal.Oscillator.PlanTriggers;
using XMJ.Educational.Oscillator.Works;

namespace XMJ.Educational.Oscillator.Schedules
{
    [AutoWorkData]
    public class UpdateClassAndClassStudentIsValidSchedule : BaseOscillatorSchedule<UpdateClassAndClassStudentIsValidWorkData>, IOscillatorSchedule
    {
        public override string Name => ""更新班级有效状态"";
        public override string? WorkDescription => ""根据非课时规则更新班级、班级学生的有效状态"";
        public override AddPlanModel GetPlan() => new()//每天凌晨1点执行
        {
            Name = ""计划1"",
            PlanTriggerData = new RepeatPlanTrigger()
            {
                DateTrigger = new DateDayTrigger(),
                EveryDayTrigger = new EveryDayOneTimeTrigger() { StartTime = new Time(1, 0, 0) }
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
