using Materal.Oscillator.Abstractions.Extensions;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Materal.Oscillator.Abstractions.Oscillators
{
    /// <summary>
    /// 调度器基类
    /// </summary>
    public abstract class OscillatorBase<TOscillatorData> : IOscillator
        where TOscillatorData : class, IOscillatorData, new()
    {
        /// <inheritdoc/>
        public Guid ID => Data.ID;
        /// <inheritdoc/>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取调度器类型数据名称失败");
        /// <inheritdoc/>
        public Type DataType => typeof(TOscillatorData);
        /// <inheritdoc/>
        public IOscillatorData OscillatorData => Data;
        /// <summary>
        /// 调度器数据
        /// </summary>
        public TOscillatorData Data { get; private set; } = new();
        /// <summary>
        /// 根步骤
        /// </summary>
        public IWork? Work { get; private set; }
        /// <summary>
        /// 日志对象
        /// </summary>
        protected ILogger? Logger { get; }
        /// <summary>
        /// 服务提供者
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public OscillatorBase(IServiceProvider serviceProvider)
        {
            ILoggerFactory? loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType());
            ServiceProvider = serviceProvider;
        }
        /// <inheritdoc/>
        public virtual async Task ExecuteAsync(IJobExecutionContext jobExecutionContext, IPlanTriggerData planTriggerData)
        {
            IOscillatorContext oscillatorContext = CreateOscillatorContext(jobExecutionContext, planTriggerData);
            await OnOscillatorContextInitAsynnc(oscillatorContext);
            Stopwatch stopwatch = new();
            try
            {
                oscillatorContext.StartTime = DateTime.Now;
                await OnOscillatorStartAsync(oscillatorContext);
                stopwatch.Start();
                if (Work is null) return;
                await Work.ExecuteAsync(oscillatorContext);
            }
            catch (Exception ex)
            {
                oscillatorContext.IsSuccess = false;
                oscillatorContext.Exception = ex;
            }
            finally
            {
                if (oscillatorContext.Exception is null && Work is not null)
                {
                    if (oscillatorContext.IsSuccess)
                    {
                        await Work.SuccessAsync(oscillatorContext);
                    }
                    else
                    {
                        await Work.FailAsync(oscillatorContext);
                    }
                }
                stopwatch.Stop();
                oscillatorContext.ElapsedTime = stopwatch.ElapsedMilliseconds;
                oscillatorContext.EndTime = DateTime.Now;
                await OnOscillatorEndAsync(oscillatorContext);
            }
        }
        /// <summary>
        /// 创建调度器上下文
        /// </summary>
        /// <param name="jobExecutionContext"></param>
        /// <param name="planTriggerData"></param>
        /// <returns></returns>
        protected abstract IOscillatorContext CreateOscillatorContext(IJobExecutionContext jobExecutionContext, IPlanTriggerData planTriggerData);
        /// <inheritdoc/>
        public async Task SetDataAsync(IOscillatorData data)
        {
            if (data is not TOscillatorData trueData) throw new OscillatorException("数据类型错误");
            Data = trueData;
            Work = await data.Work.GetWorkAsync(ServiceProvider);
            await InitAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected virtual async Task InitAsync() => await Task.CompletedTask;
        /// <summary>
        /// 初始化调度器上下文
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnOscillatorContextInitAsynnc(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnOscillatorContextInitAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 调度器开始
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnOscillatorStartAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnOscillatorStartAsync(oscillatorContext, this);
            }
        }
        /// <summary>
        /// 调度器结束
        /// </summary>
        /// <param name="oscillatorContext"></param>
        /// <returns></returns>
        protected async Task OnOscillatorEndAsync(IOscillatorContext oscillatorContext)
        {
            foreach (IOscillatorListener oscillatorListener in oscillatorContext.Listeners)
            {
                await oscillatorListener.OnOscillatorEndAsync(oscillatorContext, this);
            }
        }
    }
}
