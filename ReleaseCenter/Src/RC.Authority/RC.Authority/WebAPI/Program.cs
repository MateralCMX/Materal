using RC.Core.WebAPI;
using Materal.Common;
using Materal.TTA.EFRepository;
using RC.Authority.RepositoryImpl;
using RC.Authority.Common;
using RC.Authority.Services;

namespace RC.Authority.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program : BaseProgram
    {
        /// <summary>
        /// 入口函数
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
            #region 添加默认用户
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            userService = null;
            #endregion
            await app.RunAsync();
        }
    }
}