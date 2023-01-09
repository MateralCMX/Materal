using RC.Core.WebAPI;

namespace RC.Demo.WebAPI
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
                //services.AddDemoService();
            },
            appconfig =>
            {
            }, "测试系统");
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