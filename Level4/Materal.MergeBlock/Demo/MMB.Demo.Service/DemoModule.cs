using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using MMB.Demo.Repository;
using System.Reflection;

namespace Materal.MergeBlock.Authorization
{
    public class DemoModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            //if (context.Configuration is not IConfigurationBuilder configuration) return;
            //string? url = context.Configuration.GetValue("ConfigUrl");
            //if (url is null || !url.IsUrl()) throw new MergeBlockException("配置中心地址错误");
            //configuration.AddDefaultNameSpace(url, "MMBProject")
            //    .AddNameSpace("Demo");
            await base.OnConfigServiceBeforeAsync(context);
        }
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            SqliteConfigModel sqliteConfig = new()
            {
                Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MMBDemo.db"),
            };
            context.Services.AddTTASqliteEFRepository<DemoDBContext>(sqliteConfig.ConnectionString, Assembly.Load("MMB.Demo.Repository"));
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
#warning 仓储模块未完成，DI模块有错
            using IServiceScope serviceScope = context.ServiceProvider.CreateScope();
            IMigrateHelper migrateHelper = serviceScope.ServiceProvider.GetRequiredService<IMigrateHelper<DemoDBContext>>();
            await migrateHelper.MigrateAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
