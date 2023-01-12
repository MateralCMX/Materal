using Materal.Common;
using Materal.TTA.EFRepository;
using MBC.Core.WebAPI;
using MBC.Demo.EFRepository;
using MBC.Demo.Services;

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
            }, null, "MBC.Demo");
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