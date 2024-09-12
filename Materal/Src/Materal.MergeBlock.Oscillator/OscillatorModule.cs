using Materal.MergeBlock.Abstractions.Extensions;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器模块
    /// </summary>
    public class OscillatorModule() : MergeBlockModule("调度器模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddOscillator();
            context.Services.AddMergeBlockHostedService<OscillatorHostedService>();
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            await context.ServiceProvider.UseOscillatorAsync();
        }
    }
}
