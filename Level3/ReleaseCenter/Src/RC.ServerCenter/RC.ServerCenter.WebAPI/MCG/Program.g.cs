using Materal.Common;
using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.ServerCenter.EFRepository;

namespace RC.ServerCenter.WebAPI
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
                new ServerCenterDIManager().AddServerCenterService(services);
                program.ConfigService(services);
            }, program.ConfigApp, "RC.ServerCenter");
            MateralServices.Services ??= app.Services;
            MigrateHelper<ServerCenterDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<ServerCenterDBContext>>();
            await migrateHelper.MigrateAsync();
            await program.InitAsync(args, app.Services, app);
            await app.RunAsync();
        }
    }
}
