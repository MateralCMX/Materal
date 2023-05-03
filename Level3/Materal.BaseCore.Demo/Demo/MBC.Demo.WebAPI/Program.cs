using Materal.Abstractions;
using Materal.TTA.EFRepository;
using MBC.Demo.EFRepository;
using MBC.Demo.Services;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region Ǩ�����ݿ�
            IMigrateHelper<DemoDBContext> migrateHelper = services.GetRequiredService<IMigrateHelper<DemoDBContext>>();
            await migrateHelper.MigrateAsync();
            #endregion
            #region ���Ĭ���û�
            IUserService? userService = services.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await base.InitAsync(args, services, app);
        }
    }
}