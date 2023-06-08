using Materal.TFMS.EventBus;

namespace RC.ServerCenter.WebAPI
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
            services.GetRequiredService<IEventBus>().StartListening();
            await base.InitAsync(args, services, app);
        }
    }
}