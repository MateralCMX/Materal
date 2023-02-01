using Materal.Common;
using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.Demo.EFRepository;
using RC.Demo.Services;

namespace RC.Demo.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program : RCProgram
    {
        /// <summary>
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = RCStart(args, services =>
            {
                services.AddDemoService();
            }, null, "RC.Demo");
            MigrateHelper<DemoDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<DemoDBContext>>();
            await migrateHelper.MigrateAsync();
            #region ���Ĭ���û�
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await app.RunAsync();
        }
    }
}