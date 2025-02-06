using Materal.Oscillator.Abstractions.Extensions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器作业
    /// </summary>
    internal class OscillatorJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using IServiceScope scope = OscillatorServices.ServiceProvider.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;
            object oscillatorObj = context.JobDetail.JobDataMap.Get(ConstData.OscillatorKey);
            if (oscillatorObj is null || oscillatorObj is not IOscillatorData oscillatorData) return;
            IOscillator? oscillator = await oscillatorData.GetOscillatorAsync(serviceProvider);
            if (oscillator is null) return;
            string[] triggerSplitValues = context.Trigger.Key.Name.Split('_');
            string triggerName = string.Empty;
            if (triggerSplitValues.Length < 2 || !triggerSplitValues[0].IsGuid()) throw new OscillatorException("获取计划触发器信息失败:TriggerKey格式错误");
            Guid triggerID = Guid.Parse(triggerSplitValues[0]);
            IPlanTriggerData planTriggerData = oscillatorData.PlanTriggers.FirstOrDefault(x => x.ID == triggerID) ?? throw new OscillatorException("获取计划触发器信息失败:未找到计划触发器");
            await oscillator.ExecuteAsync(context, planTriggerData);
        }
    }
}
