using RC.Core.WebAPI;

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
                //services.AddDemoService();
            },
            appconfig =>
            {
            }, "����ϵͳ");
            //MigrateHelper<DemoDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<DemoDBContext>>();
            //await migrateHelper.MigrateAsync();
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