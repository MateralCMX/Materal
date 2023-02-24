using Materal.Abstractions;
using Materal.TTA.EFRepository;
using MBC.Core.WebAPI;
using MBC.Demo.EFRepository;
using MBC.Demo.Services;
using MBC.Demo.WebAPI.Hubs;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program : MBCProgram
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = MBCStart(args, services =>
            {
                services.AddDemoService();
            }, "MBC.Demo");
            app.MapHub<SomeHub>("/hubs/Some");
            MigrateHelper<DemoDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<DemoDBContext>>();
            await migrateHelper.MigrateAsync();
            #region 添加默认用户
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await app.RunAsync();
        }
    }
}