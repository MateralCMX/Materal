using Materal.MergeBlock.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器服务
    /// </summary>
    public class OscillatorHostedService(IOscillatorHost oscillatorHost, IServiceProvider serviceProvider, MergeBlockContext mergeBlockContext) : IHostedService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
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
                    await oscillatorHost.InitWorkAsync(oscillator.Work);
                }
                foreach (IOscillatorData oscillator in oscillators)
                {
                    await oscillatorHost.StartOscillatorAsync(oscillator);
                }
            });
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
