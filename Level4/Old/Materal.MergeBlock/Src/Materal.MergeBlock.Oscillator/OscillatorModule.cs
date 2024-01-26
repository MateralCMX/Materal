using Materal.Oscillator.SqliteEFRepository;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using static System.Formats.Asn1.AsnWriter;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器模块
    /// </summary>
    public class OscillatorModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            IEnumerable<Assembly> allAssemblies = context.ModuleInfos.Select(m => m.ModuleAssembly);
            List<Assembly> oscillatorAssemblies = [];
            foreach (Assembly assembly in allAssemblies)
            {
                if (!assembly.GetTypes().Any(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<IOscillatorSchedule>())) continue;
                oscillatorAssemblies.Add(assembly);
            }
            context.Services.AddOscillator([.. oscillatorAssemblies]);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            using IServiceScope scope = context.ServiceProvider.CreateScope();
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<OscillatorDBContext>>();
            await migrateHelper.MigrateAsync();
            IOscillatorService oscillatorService = scope.ServiceProvider.GetRequiredService<IOscillatorService>();
            await oscillatorService.StartAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
