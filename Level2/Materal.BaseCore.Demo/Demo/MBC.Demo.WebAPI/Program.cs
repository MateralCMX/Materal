using Materal.Abstractions;
using MBC.Demo.Services;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region 添加默认用户
            IUserService? userService = MateralServices.GetService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await base.InitAsync(args, services, app);
        }
    }
}