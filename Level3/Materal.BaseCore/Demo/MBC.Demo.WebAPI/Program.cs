using Materal.TTA.EFRepository;
using MBC.Core.WebAPI;
using MBC.Demo.EFRepository;
using MBC.Demo.Services;
using MBC.Demo.WebAPI.Hubs;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program : MBCProgram
    {
        /// <summary>
        /// ��ں���
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
            #region ���Ĭ���û�
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IMigrateHelper<DemoDBContext> migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<DemoDBContext>>();
                await migrateHelper.MigrateAsync();
                IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                await userService.AddDefaultUserAsync();
            }
            #endregion
            await app.RunAsync();
        }
    }
}