using Materal.MergeBlock.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器服务
    /// </summary>
    public class OscillatorHostedService(IOscillatorHost oscillatorHost, IServiceProvider serviceProvider, MergeBlockContext mergeBlockContext, ILogger<OscillatorHostedService>? logger = null) : IHostedService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger?.LogInformation("调度器启动中...");
            List<IOscillatorData> oscillators = [];
            foreach (Assembly assembly in mergeBlockContext.MergeBlockAssemblies)
            {
                Type[] workDataTypes = assembly.GetTypes<IMergeBlockWorkData>().ToArray();
                if (workDataTypes.Length <= 0) continue;
                foreach (Type workDataType in workDataTypes)
                {
                    IMergeBlockWorkData workData = workDataType.InstantiationOrDefault<IMergeBlockWorkData>(serviceProvider) ?? throw new OscillatorException("实例化WorkData失败");
                    ICollection<IPlanTriggerData> planTriggers = workData.GetPlanTriggers();
                    NormalOscillatorData oscillator = new()
                    {
                        Work = workData,
                        PlanTriggers = [.. planTriggers]
                    };
                    oscillators.Add(oscillator);
                }
            }
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                foreach (IOscillatorData oscillator in oscillators)
                {
                    logger?.LogDebug($"任务[{oscillator.Work.Name}_{oscillator.Work.GetType().FullName}]初始化");
                    await oscillatorHost.InitWorkAsync(oscillator.Work);
                    logger?.LogDebug($"任务[{oscillator.Work.Name}_{oscillator.Work.GetType().FullName}]初始化成功");
                }
                foreach (IOscillatorData oscillator in oscillators)
                {
                    logger?.LogDebug($"任务[{oscillator.Work.Name}_{oscillator.Work.GetType().FullName}]启动");
                    await oscillatorHost.StartOscillatorAsync(oscillator);
                    logger?.LogDebug($"任务[{oscillator.Work.Name}_{oscillator.Work.GetType().FullName}]启动成功");
                }
            });
            logger?.LogInformation("调度器启动成功");
            await Task.CompletedTask;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken) => await oscillatorHost.StopAsync();
    }
}
