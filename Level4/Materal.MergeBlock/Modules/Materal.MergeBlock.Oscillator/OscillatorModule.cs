﻿using Materal.Oscillator.SqliteEFRepository;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.MergeBlock.Oscillator
{
    /// <summary>
    /// 调度器模块
    /// </summary>
    public class OscillatorModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorModule():base("调度器模块", "Oscillator")
        {

        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            IEnumerable<Assembly> allAssemblies = MergeBlockHost.ModuleInfos.Select(m => m.ModuleType.Assembly);
            List<Assembly> oscillatorAssemblies = [];
            foreach (Assembly assembly in allAssemblies)
            {
                if (!assembly.GetTypes<IOscillatorSchedule>().Any()) continue;
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
