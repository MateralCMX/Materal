using Materal.ContextCache;
using Materal.Logger.Abstractions;
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
    internal class OscillatorJob : IJob, IContextRestorer
    {
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OscillatorJob> _logger;
        private readonly Dictionary<string, object?> _loggerScopeData;
        private const string PlanTriggerNameKey = "PlanTriggerName";
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorJob()
        {
            _scope = OscillatorServices.ServiceProvider.CreateScope();
            _serviceProvider = _scope.ServiceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<OscillatorJob>>();
            _loggerScopeData = [];
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="oscillator"></param>
        /// <param name="restorerContext"></param>
        /// <returns></returns>
        private WorkContext Init(IOscillator oscillator, RestorerContext restorerContext)
        {
            foreach (KeyValuePair<string, object?> data in restorerContext.LoggerScopeData)
            {
                _loggerScopeData.TryAdd(data.Key, data.Value);
            }
            restorerContext.LoggerScopeData = _loggerScopeData;
            WorkContext workContext = new(_serviceProvider, oscillator, _loggerScopeData);
            workContext.LoggerScopeData.TryAdd("StartTime", DateTime.Now);
            workContext.LoggerScopeData.TryAdd("OscillatorID", oscillator.ID);
            workContext.LoggerScopeData.TryAdd("WorkID", oscillator.WorkData.ID);
            workContext.LoggerScopeData.TryAdd("WorkName", oscillator.WorkData.Name);
            return workContext;
        }
        /// <summary>
        /// 结束收尾
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="contextCache"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task EndAsync(WorkContext workContext, IContextCache contextCache, Exception? exception)
        {
            LoggerScope loggerScope = new("Oscillator", _loggerScopeData);
            using IDisposable? _ = _logger.BeginScope(loggerScope);
            if (exception is null)
            {
                if (workContext.Exception is null)
                {
                    _logger.LogInformation($"调度器[${{PlanTriggerName}}_{workContext.Oscillator.WorkData.Name}]执行完毕");
                }
                else
                {
                    _logger.LogError($"调度器[${{PlanTriggerName}}_{workContext.Oscillator.WorkData.Name}]执行失败");
                }
            }
            else
            {
                string? triggerName;
                if (workContext.LoggerScopeData.TryGetValue(PlanTriggerNameKey, out object? triggerNameObj))
                {
                    if (triggerNameObj is string tempTriggerName)
                    {
                        triggerName = tempTriggerName;
                    }
                    else
                    {
                        triggerName = triggerNameObj?.ToString();
                    }
                }
                else
                {
                    triggerName = workContext.Oscillator.Triggers.FirstOrDefault()?.Name;
                }
                if (string.IsNullOrWhiteSpace(triggerName))
                {
                    triggerName = "未知任务";
                }
                _logger.LogError(exception, $"调度器[{triggerName}_{workContext.Oscillator.WorkData.Name}]发生错误");
            }
            await contextCache.DisposeAsync();
            _scope.Dispose();
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="restorerContext"></param>
        /// <param name="contextCache"></param>
        /// <returns></returns>
        private async Task ExecuteWorkAsync(WorkContext workContext, RestorerContext restorerContext, IContextCache contextCache)
        {
            #region 准备工作
            IWork work = OscillatorConvertHelper.CreateNewWork(workContext.Oscillator.WorkData, _serviceProvider);
            workContext.LoggerScopeData.TryAdd("WorkType", work.TypeName);
            string[] triggerSplitValues = restorerContext.TriggerName.Split('_');
            string triggerName = string.Empty;
            if (triggerSplitValues.Length >= 2)
            {
                try
                {
                    Guid triggerID = Guid.Parse(triggerSplitValues[0]);
                    triggerName = restorerContext.TriggerName[(triggerSplitValues[0].Length + 1)..];
                    workContext.LoggerScopeData.TryAdd("PlanTriggerID", triggerID);
                    workContext.PlanTriggerID = triggerID;
                    workContext.LoggerScopeData.TryAdd(PlanTriggerNameKey, triggerName);
                    foreach (IPlanTrigger planTrigger in workContext.Oscillator.Triggers)
                    {
                        if (planTrigger.ID != triggerID) continue;
                        string planTriggerDescription = planTrigger.GetDescriptionText();
                        workContext.LoggerScopeData.Add("PlanTriggerDescription", planTriggerDescription);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    workContext.LoggerScopeData.TryAdd("WarrningMessage", $"获取计划触发器信息失败:{ex.GetErrorMessage()}");
                }
            }
            else
            {
                workContext.LoggerScopeData.TryAdd("WarrningMessage", "获取计划触发器信息失败:TriggerKey格式错误");
            }
            #endregion
            await ExcuteWorkAsync(workContext, restorerContext, contextCache, work);
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="restorerContext"></param>
        /// <param name="contextCache"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        private static async Task ExcuteWorkAsync(WorkContext workContext, RestorerContext restorerContext, IContextCache contextCache, IWork work)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            try
            {
                if (restorerContext.Step == OscillatorJobStep.RunWork)
                {
                    await work.ExecuteAsync(workContext);
                    restorerContext.Step = OscillatorJobStep.Success;
                    restorerContext.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    contextCache.NextStep();
                    restorerContext.ElapsedMilliseconds = 0;
                }
                else
                {
                    workContext.Exception = restorerContext.Exception;
                }
            }
            catch (Exception ex)
            {
                workContext.Exception = ex.GetErrorMessage();
                restorerContext.Exception = workContext.Exception;
                restorerContext.Step = OscillatorJobStep.Fail;
                restorerContext.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                contextCache.NextStep();
                restorerContext.ElapsedMilliseconds = 0;
            }
            finally
            {
                if (string.IsNullOrWhiteSpace(workContext.Exception))
                {
                    await ExcuteSuccessWorkAsync(workContext, work);
                }
                else
                {
                    await ExcuteFailWorkAsync(workContext, work);
                }
                stopwatch.Stop();
                workContext.LoggerScopeData.TryAdd("ElapsedTime", restorerContext.ElapsedMilliseconds + stopwatch.ElapsedMilliseconds);
                workContext.LoggerScopeData.TryAdd("EndTime", DateTime.Now);
            }
        }
        /// <summary>
        /// 执行成功任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        private static async Task ExcuteSuccessWorkAsync(WorkContext workContext, IWork work)
        {
            workContext.LoggerScopeData.TryAdd("IsSuccess", true);
            try
            {
                await work.SuccessExecuteAsync(workContext);
            }
            catch (Exception ex)
            {
                workContext.LoggerScopeData.TryAdd("SuccessException", ex.GetErrorMessage());
            }
        }
        /// <summary>
        /// 执行失败任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        private static async Task ExcuteFailWorkAsync(WorkContext workContext, IWork work)
        {
            workContext.LoggerScopeData.TryAdd("IsSuccess", false);
            try
            {
                await work.FailExecuteAsync(workContext);
            }
            catch (Exception ex)
            {
                workContext.LoggerScopeData.TryAdd("FailException", ex.GetErrorMessage());
            }
        }
        /// <summary>
        /// 重新开始
        /// </summary>
        /// <param name="contextCache"></param>
        /// <returns></returns>
        public async Task RenewAsync(IContextCache contextCache)
        {
            if (contextCache.Context is not RestorerContext restorerContext || string.IsNullOrWhiteSpace(restorerContext.OscillatorData)) return;
            IOscillator oscillator = await OscillatorConvertHelper.DeserializationAsync<IOscillator>(restorerContext.OscillatorData, _serviceProvider);
            WorkContext workContext = Init(oscillator, restorerContext);
            Exception? exception = null;
            try
            {
                await ExecuteWorkAsync(workContext, restorerContext, contextCache);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                await EndAsync(workContext, contextCache, exception);
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            object oscillatorObj = context.JobDetail.JobDataMap.Get(ConstData.OscillatorKey);
            if (oscillatorObj is null || oscillatorObj is not IOscillator oscillator) return;
            RestorerContext restorerContext = new()
            {
                OscillatorData = await oscillator.SerializationAsync(),
                LoggerScopeData = _loggerScopeData,
                TriggerName = context.Trigger.Key.Name
            };
            IContextCacheService contextCacheService = _serviceProvider.GetRequiredService<IContextCacheService>();
            IContextCache contextCache = await contextCacheService.BeginContextCacheAsync<OscillatorJob, RestorerContext>(restorerContext);
            WorkContext workContext = Init(oscillator, restorerContext);
            Exception? exception = null;
            try
            {
                await ExecuteWorkAsync(workContext, restorerContext, contextCache);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                await EndAsync(workContext, contextCache, exception);
            }
        }
    }
}
