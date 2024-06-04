using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器服务
    /// </summary>
    /// <param name="oscillatorHost"></param>
    /// <param name="serviceProvider"></param>
    public class OscillatorHostedService(IOscillatorHost oscillatorHost, IServiceProvider serviceProvider) : IHostedService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Assembly> allAssemblies = MergeBlockHost.ModuleInfos.Select(m => m.ModuleType.Assembly);
            List<IOscillator> oscillators = [];
            foreach (Assembly assembly in allAssemblies)
            {
                Type[] workDataTypes = assembly.GetTypes<IMergeBlockWorkData>().ToArray();
                if (workDataTypes.Length <= 0) continue;
                foreach (Type workDataType in workDataTypes)
                {
                    IMergeBlockWorkData workData = workDataType.InstantiationOrDefault<IMergeBlockWorkData>(serviceProvider) ?? throw new OscillatorException("实例化WorkData失败");
                    ICollection<IPlanTrigger> planTriggers = workData.GetPlanTriggers();
                    DefaultOscillator oscillator = new(workData, [.. planTriggers]);
                    oscillators.Add(oscillator);
                }
            }
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                foreach (IOscillator oscillator in oscillators)
                {
                    await oscillatorHost.InitWorkAsync(oscillator.WorkData);
                }
                foreach (IOscillator oscillator in oscillators)
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
