using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.Demo.EFRepository;

namespace RC.Demo.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public partial class Program : RCProgram
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Program program = new();
            WebApplication app = RCProgram.RCStart(args, services =>
            {
                new DemoDIManager().AddDemoService(services);
                program.ConfigService(services);
            }, program.ConfigApp, "RC.Demo");
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IMigrateHelper<DemoDBContext> migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<DemoDBContext>>();
                await migrateHelper.MigrateAsync();
                await program.InitAsync(args, scope.ServiceProvider, app);
            }
            await app.RunAsync();
        }
    }
}
