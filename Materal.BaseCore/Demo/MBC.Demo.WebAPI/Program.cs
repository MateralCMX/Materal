using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Materal.Common;
using Materal.Logger;
using Materal.TTA.EFRepository;
using MBC.Demo.Services;
using MBC.Demo.SqliteEFRepository;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program : BaseProgram
    {
        /// <summary>
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = Start(args, config =>
            {
                config.AddJsonFile("MBCConfig.json", false, true);
            }, services =>
            {
                services.AddDemoService();
            }, configApp =>
            {
                MateralLoggerManager.CustomConfig.Add("ApplicationName", WebAPIConfig.AppName);
            }, "MBC.Demo");
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