using Materal.Abstractions;
using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.EnvironmentServer.EFRepository;

namespace RC.EnvironmentServer.WebAPI
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
                new EnvironmentServerDIManager().AddEnvironmentServerService(services);
                program.ConfigService(services);
            }, program.ConfigApp, "RC.EnvironmentServer");
            MateralServices.Services ??= app.Services;
            MigrateHelper<EnvironmentServerDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<EnvironmentServerDBContext>>();
            await migrateHelper.MigrateAsync();
            await program.InitAsync(args, app.Services, app);
            await app.RunAsync();
        }
    }
}
