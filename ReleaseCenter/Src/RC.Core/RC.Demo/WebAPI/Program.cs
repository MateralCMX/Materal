using RC.Core.WebAPI;
using Materal.Common;
using Materal.TTA.EFRepository;
using RC.Demo.RepositoryImpl;
using RC.Demo.Common;

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
            WebApplication app = Start(args, services =>
            {
                services.AddDemoService();
            }, "����ϵͳ");
            var a = ApplicationConfig.DBConfig.ConnectionString;
            MigrateHelper<DemoDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<DemoDBContext>>();
            await migrateHelper.MigrateAsync();
            //IEventBus? eventBus = MateralServices.GetServiceOrDefatult<IEventBus>();
            //if (eventBus != null)
            //{
            //    await eventBus.SubscribeAsync<TestEvent, TestEventHandler>();
            //    eventBus.StartListening();
            //}
            await app.RunAsync();
        }
    }
}