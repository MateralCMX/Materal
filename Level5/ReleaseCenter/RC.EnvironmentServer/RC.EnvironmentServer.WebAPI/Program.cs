using Materal.TFMS.EventBus;
using RC.EnvironmentServer.Services;

namespace RC.EnvironmentServer.WebAPI
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
            #region ��ʼ������
            IConfigurationItemService configurationItemService = services.GetRequiredService<IConfigurationItemService>();
            await configurationItemService.InitAsync();
            #endregion
            services.GetRequiredService<IEventBus>().StartListening();
            await base.InitAsync(args, services, app);
        }
    }
}