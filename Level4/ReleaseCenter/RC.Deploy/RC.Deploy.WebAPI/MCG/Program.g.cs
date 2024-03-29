using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.Deploy.EFRepository;

namespace RC.Deploy.WebAPI
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
                new DeployDIManager().AddDeployService(services);
                program.ConfigService(services);
            }, program.ConfigApp, program.ConfigBuilder, "RC.Deploy");
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IMigrateHelper<DeployDBContext> migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<DeployDBContext>>();
                await migrateHelper.MigrateAsync();
                await program.InitAsync(args, scope.ServiceProvider, app);
            }
            await app.RunAsync();
        }
    }
}
