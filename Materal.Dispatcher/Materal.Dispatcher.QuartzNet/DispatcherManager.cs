using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Xml;
using System;
using System.Threading.Tasks;

namespace Materal.Dispatcher.QuartzNet
{
    public class DispatcherManager : IDispatcherManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IScheduleManager _scheduleManager;
        private IScheduler _scheduler;

        public DispatcherManager(IServiceProvider serviceProvider, IScheduleManager scheduleManager)
        {
            _serviceProvider = serviceProvider;
            _scheduleManager = scheduleManager;
        }

        public async Task Start(DispatcherConfigModel config)
        {
            if (config.EnableLog)
            {
                var logProvider = _serviceProvider.GetService<ILogProvider>();
                if (logProvider != null) LogProvider.SetCurrentLogProvider(logProvider);
            }
            _scheduler = await _scheduleManager.BuildScheduler(config.SchedulerConfig);
            var processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
            await processor.ProcessFileAndScheduleJobs(config.DispatcherConfigFilePath, _scheduler);
            await _scheduler.Start();
        }

        public async Task Stop()
        {
            if (!_scheduler.IsShutdown) await _scheduler.Shutdown();
        }
    }
}
