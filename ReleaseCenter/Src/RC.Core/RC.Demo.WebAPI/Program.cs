using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Materal.Common;
using Materal.Logger;
using Materal.TTA.EFRepository;
using RC.Demo.EFRepository;
using RC.Demo.Services;

namespace RC.Demo.WebAPI
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
                config.AddJsonFile("RCConfig.json", false, true);
            }, services =>
            {
                services.AddDemoService();
            }, configApp =>
            {
                MateralLoggerManager.CustomConfig.Add("ApplicationName", WebAPIConfig.AppName);
            }, "RC.Demo");
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