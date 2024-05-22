using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器模块
    /// </summary>
    public class OscillatorModule() : MergeBlockModule("调度器模块", "Oscillator")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddOscillator();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            await context.ServiceProvider.UseOscillatorAsync();
            IOscillatorHost oscillatorHost = context.ServiceProvider.GetRequiredService<IOscillatorHost>();
            using IServiceScope scope = context.ServiceProvider.CreateScope();
            IEnumerable<Assembly> allAssemblies = MergeBlockHost.ModuleInfos.Select(m => m.ModuleType.Assembly);
            List<IOscillator> oscillators = [];
            foreach (Assembly assembly in allAssemblies)
            {
                Type[] workDataTypes = assembly.GetTypes<IMergeBlockWorkData>().ToArray();
                if (workDataTypes.Length <= 0) continue;
                foreach (Type workDataType in workDataTypes)
                {
                    IMergeBlockWorkData workData = workDataType.InstantiationOrDefault<IMergeBlockWorkData>(scope.ServiceProvider) ?? throw new OscillatorException("实例化WorkData失败");
                    ICollection<IPlanTrigger> planTriggers = workData.GetPlanTriggers();
                    DefaultOscillator oscillator = new(workData, [.. planTriggers]);
                    await oscillatorHost.InitWorkAsync(workData);
                    oscillators.Add(oscillator);
                }
            }
            foreach (IOscillator oscillator in oscillators)
            {
                await oscillatorHost.StartOscillatorAsync(oscillator);
            }
        }
    }
}
