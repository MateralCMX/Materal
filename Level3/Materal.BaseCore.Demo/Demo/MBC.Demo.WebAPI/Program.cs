using Materal.BaseCore.EventBus;
using Materal.BaseCore.WebAPI.Common;
using MBC.Demo.Services;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigService(IServiceCollection services)
        {
            base.ConfigService(services);
            services.AddEventBus($"{WebAPIConfig.AppName}Queue", true);
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region ���Ĭ���û�
            IUserService? userService = services.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await base.InitAsync(args, services, app);
        }
        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="builder"></param>
        public override void ConfigBuilder(WebApplicationBuilder builder)
        {
            base.ConfigBuilder(builder);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 256 * 1024 * 1024;
            });
        }
    }
}