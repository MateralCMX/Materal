using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Works;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器作业
    /// </summary>
    internal class OscillatorJob : IJob
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            object oscillatorObj = context.JobDetail.JobDataMap.Get(ConstData.OscillatorKey);
            if (oscillatorObj is null || oscillatorObj is not IOscillator oscillator) return;
            using IServiceScope scope = OscillatorServices.ServiceProvider.CreateScope();
            ILogger<OscillatorJob> logger = scope.ServiceProvider.GetRequiredService<ILogger<OscillatorJob>>();
            WorkContext workContext = new(scope.ServiceProvider, oscillator);
            using IDisposable? loggerDisposable = logger.BeginScope(workContext.LoggerScope);
            workContext.LoggerScope.ScopeData.TryAdd("StartTime", DateTime.Now);
            workContext.LoggerScope.ScopeData.TryAdd("OscillatorID", oscillator.ID);
            workContext.LoggerScope.ScopeData.TryAdd("WorkID", oscillator.WorkData.ID);
            workContext.LoggerScope.ScopeData.TryAdd("WorkName", oscillator.WorkData.Name);
            IWork work = OscillatorConvertHelper.CreateNewWork(oscillator.WorkData, scope.ServiceProvider);
            workContext.LoggerScope.ScopeData.TryAdd("WorkType", work.TypeName);
            string[] triggerSplitValues = context.Trigger.Key.Name.Split('_');
            string triggerName = string.Empty;
            if (triggerSplitValues.Length >= 2)
            {
                try
                {
                    Guid triggerID = Guid.Parse(triggerSplitValues[0]);
                    triggerName = context.Trigger.Key.Name[(triggerSplitValues[0].Length + 1)..];
                    workContext.LoggerScope.ScopeData.TryAdd("PlanTriggerID", triggerID);
                    workContext.PlanTriggerID = triggerID;
                    workContext.LoggerScope.ScopeData.TryAdd("PlanTriggerName", triggerName);
                    foreach (IPlanTrigger planTrigger in oscillator.Triggers)
                    {
                        if (planTrigger.ID != triggerID) continue;
                        string planTriggerDescription = planTrigger.GetDescriptionText();
                        workContext.LoggerScope.ScopeData.Add("PlanTriggerDescription", planTriggerDescription);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    workContext.LoggerScope.ScopeData.TryAdd("WarrningMessage", $"获取计划触发器信息失败:{ex.GetErrorMessage()}");
                }
            }
            else
            {
                workContext.LoggerScope.ScopeData.TryAdd("WarrningMessage", "获取计划触发器信息失败:TriggerKey格式错误");
            }
            Stopwatch stopwatch = new();
            stopwatch.Start();
            try
            {
                await work.ExecuteAsync(workContext);
            }
            catch (Exception ex)
            {
                workContext.Exception = ex;
            }
            finally
            {
                if (workContext.Exception is null)
                {
                    workContext.LoggerScope.ScopeData.TryAdd("IsSuccess", true);
                    try
                    {
                        await work.SuccessExecuteAsync(workContext);
                    }
                    catch (Exception ex)
                    {
                        workContext.LoggerScope.ScopeData.TryAdd("SuccessException", ex.GetErrorMessage());
                    }
                }
                else
                {
                    workContext.LoggerScope.ScopeData.TryAdd("IsSuccess", false);
                    try
                    {
                        await work.FailExecuteAsync(workContext);
                    }
                    catch (Exception ex)
                    {
                        workContext.LoggerScope.ScopeData.TryAdd("FailException", ex.GetErrorMessage());
                    }
                }
                stopwatch.Stop();
                workContext.LoggerScope.ScopeData.TryAdd("ElapsedTime", stopwatch.ElapsedMilliseconds);
                workContext.LoggerScope.ScopeData.TryAdd("EndTime", DateTime.Now);
                if (workContext.Exception is null)
                {
                    logger.LogInformation($"调度器[{triggerName}_{oscillator.WorkData.Name}]执行完毕");
                }
                else
                {
                    logger.LogError(workContext.Exception, $"调度器[{triggerName}_{oscillator.WorkData.Name}]执行失败");
                }
            }
        }
    }
}
