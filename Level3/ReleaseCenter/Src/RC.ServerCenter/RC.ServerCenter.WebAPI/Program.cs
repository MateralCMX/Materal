using Materal.Common;
using Materal.TFMS.EventBus;
using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.ServerCenter.EFRepository;

namespace RC.ServerCenter.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program : RCProgram
    {
        /// <summary>
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = RCStart(args, services =>
            {
                services.AddServerCenterService();
            }, null, "RC.ServerCenter");
            MigrateHelper<ServerCenterDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<ServerCenterDBContext>>();
            await migrateHelper.MigrateAsync();
            IEventBus? eventBus = MateralServices.GetServiceOrDefatult<IEventBus>();
            if (eventBus != null)
            {
                eventBus.StartListening();
            }
            await app.RunAsync();
        }
    }
}