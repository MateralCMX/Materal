using RC.Core.WebAPI;
using Materal.Common;
using Materal.TTA.EFRepository;
using RC.Authority.RepositoryImpl;
using RC.Authority.Common;
using RC.Authority.Services;

namespace RC.Authority.WebAPI
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
            WebApplication app = Start(args, services =>
            {
                services.AddAuthorityService();
            }, "RC.Authority");
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