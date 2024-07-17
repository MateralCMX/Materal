using Materal.ContextCache;
using Materal.Logger.Abstractions;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Works;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Materal.Oscillator
{
    internal enum OscillatorJobStep : byte
    {
        RunWork = 0,
        Success = 1,
        Fail = 2
    }
    internal class RestorerContext
    {
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; } = string.Empty;
        /// <summary>
        /// Oscillator数据
        /// </summary>
        public string? OscillatorData { get; set; }
        /// <summary>
        /// 日志作用域
        /// </summary>
        public Dictionary<string, object?> LoggerScopeData { get; set; } = [];
        /// <summary>
        /// 耗时
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        public OscillatorJobStep Step { get; set; } = OscillatorJobStep.RunWork;
        public string? Exception { get; set; }
    }
    /// <summary>
    /// 调度器作业
    /// </summary>
    internal class OscillatorJob : IJob, IContextRestorer
    {
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OscillatorJob> _logger;
        private Dictionary<string, object?> _loggerScopeData;
        private readonly IDisposable? _loggerScope;
        public OscillatorJob()
        {
            _scope = OscillatorServices.ServiceProvider.CreateScope();
            _serviceProvider = _scope.ServiceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<OscillatorJob>>();
            _loggerScopeData = [];
            LoggerScope loggerScope = new("Oscillator", _loggerScopeData);
            _loggerScope = _logger.BeginScope(loggerScope);
        }
        private WorkContext Init(IOscillator oscillator)
        {
            WorkContext workContext = new(_serviceProvider, oscillator, _loggerScopeData);
            workContext.LoggerScopeData.TryAdd("StartTime", DateTime.Now);
            workContext.LoggerScopeData.TryAdd("OscillatorID", oscillator.ID);
            workContext.LoggerScopeData.TryAdd("WorkID", oscillator.WorkData.ID);
            workContext.LoggerScopeData.TryAdd("WorkName", oscillator.WorkData.Name);
            return workContext;
        }
        private async Task EndAsync(IContextCache contextCache)
        {
            await contextCache.DisposeAsync();
            _scope.Dispose();
            _loggerScope?.Dispose();
        }
        private async Task ExecuteAsync(WorkContext workContext, RestorerContext restorerContext, IContextCache contextCache)
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
                    workContext.LoggerScopeData.TryAdd("PlanTriggerName", triggerName);
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
            await ExcuteAsync(workContext, restorerContext, contextCache, work, triggerName);
        }
        private async Task ExcuteAsync(WorkContext workContext, RestorerContext restorerContext, IContextCache contextCache, IWork work, string triggerName)
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
                    await SuccessExcuteAsync(workContext, work);
                }
                else
                {
                    await FailExcuteAsync(workContext, work);
                }
                stopwatch.Stop();
                workContext.LoggerScopeData.TryAdd("ElapsedTime", restorerContext.ElapsedMilliseconds + stopwatch.ElapsedMilliseconds);
                workContext.LoggerScopeData.TryAdd("EndTime", DateTime.Now);
                if (workContext.Exception is null)
                {
                    _logger.LogInformation($"调度器[{triggerName}_{workContext.Oscillator.WorkData.Name}]执行完毕");
                }
                else
                {
                    _logger.LogError(workContext.Exception, $"调度器[{triggerName}_{workContext.Oscillator.WorkData.Name}]执行失败");
                }
            }
        }
        private static async Task SuccessExcuteAsync(WorkContext workContext, IWork work)
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
        private static async Task FailExcuteAsync(WorkContext workContext, IWork work)
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
        public async Task RenewAsync(IContextCache contextCache)
        {
            if (contextCache.Context is not RestorerContext restorerContext || string.IsNullOrWhiteSpace(restorerContext.OscillatorData)) return;
            IOscillator oscillator = await OscillatorConvertHelper.DeserializationAsync<IOscillator>(restorerContext.OscillatorData, _serviceProvider);
            _loggerScopeData = restorerContext.LoggerScopeData;
            WorkContext workContext = Init(oscillator);
            await ExecuteAsync(workContext, restorerContext, contextCache);
            await EndAsync(contextCache);
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
            WorkContext workContext = Init(oscillator);
            await ExecuteAsync(workContext, restorerContext, contextCache);
            await EndAsync(contextCache);
        }
    }
}
