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
            WorkContext workContext = new(scope.ServiceProvider);
            using IDisposable? loggerDisposable = logger.BeginScope(workContext.LoggerScope);
            workContext.LoggerScope.ScopeData.TryAdd("StartTime", DateTime.Now);
            workContext.LoggerScope.ScopeData.TryAdd("OscillatorID", oscillator.ID);
            workContext.LoggerScope.ScopeData.TryAdd("WorkID", oscillator.Work.ID);
            workContext.LoggerScope.ScopeData.TryAdd("WorkName", oscillator.Work.Name);
            workContext.LoggerScope.ScopeData.TryAdd("WorkType", oscillator.Work.TypeName);
            string[] triggerSplitValues = context.Trigger.Key.Name.Split('_');
            string triggerName = string.Empty;
            if (triggerSplitValues.Length == 2)
            {
                try
                {
                    Guid triggerID = Guid.Parse(triggerSplitValues[0]);
                    triggerName = triggerSplitValues[1];
                    workContext.LoggerScope.ScopeData.TryAdd("PlanTriggerID", triggerID);
                    workContext.LoggerScope.ScopeData.TryAdd("PlanTriggerName", triggerName);
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
                await oscillator.Work.ExecuteAsync(workContext);
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
                        await oscillator.Work.SuccessExecuteAsync(workContext);
                    }
                    catch
                    {
                    }
                    logger.LogInformation($"调度器[{triggerName}_{oscillator.Work.Name}]执行完毕");
                }
                else
                {
                    workContext.LoggerScope.ScopeData.TryAdd("IsSuccess", false);
                    try
                    {
                        await oscillator.Work.FailExecuteAsync(workContext);
                    }
                    catch
                    {
                    }
                    logger.LogError(workContext.Exception, $"调度器[{triggerName}_{oscillator.Work.Name}]执行失败");
                }
                stopwatch.Stop();
                workContext.LoggerScope.ScopeData.TryAdd("ElapsedTime", stopwatch.ElapsedMilliseconds);
                workContext.LoggerScope.ScopeData.TryAdd("EndTime", DateTime.Now);
            }
        }
    }
}
