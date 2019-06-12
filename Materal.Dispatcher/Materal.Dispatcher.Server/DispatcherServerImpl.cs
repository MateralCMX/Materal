using Materal.Dispatcher.QuartzNet;
using System.Threading.Tasks;

namespace Materal.Dispatcher.Server
{
    public class DispatcherServerImpl : IDispatcherServer
    {
        private readonly IDispatcherManager _dispatcherManager;
        private readonly DispatcherConfigModel _dispatcherConfigModel;
        public DispatcherServerImpl(IDispatcherManager dispatcherManager)
        {
            _dispatcherManager = dispatcherManager;
            _dispatcherConfigModel = new DispatcherConfigModel
            {
                DispatcherConfigFilePath = "~/DispatcherConfig.xml",
                EnableLog = true,
                SchedulerConfig = new SchedulerConfigModel
                {
                    EnableJobLog = true,
                    EnableSchedulerLog = true,
                    EnableTriggerLog = true,
                    EnableCustomJobFactory = true,
                    Name = "MateralDispatcherServer",
                    BindName = "QuartzScheduler",
                    Port = 8008,
                    TreadCount = 5
                }
            };
        }

        public async Task Start()
        {
            await _dispatcherManager.Start(_dispatcherConfigModel);
        }

        public async Task Stop()
        {
            await _dispatcherManager.Stop();
        }
    }
}
