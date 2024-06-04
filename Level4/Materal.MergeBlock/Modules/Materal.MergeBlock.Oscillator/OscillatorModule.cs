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
            context.Services.AddHostedService<OscillatorHostedService>();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context) => await context.ServiceProvider.UseOscillatorAsync();
    }
}
