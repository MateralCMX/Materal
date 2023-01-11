using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Materal.Common;
using Materal.Logger;
using Materal.TTA.EFRepository;
using RC.Authority.EFRepository;
using RC.Authority.Services;
using RC.Core.WebAPI;

namespace RC.Authority.WebAPI
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
                services.AddAuthorityService();
            }, null, "RC.Authority");
            MigrateHelper<AuthorityDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<AuthorityDBContext>>();
            await migrateHelper.MigrateAsync();
            #region ���Ĭ���û�
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            userService = null;
            #endregion
            await app.RunAsync();
        }
    }
}