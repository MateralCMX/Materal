using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz.Spi;

namespace Materal.Dispatcher.QuartzNet
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduleManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<IScheduler> BuildScheduler(SchedulerConfigModel config)
        {
            var nameValueCollection = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = config.Name,
                // 设置线程池
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = config.TreadCount.ToString(),
                ["quartz.threadPool.threadPriority"] = "Normal",
                // 远程输出配置
                ["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz",
                ["quartz.scheduler.exporter.port"] = config.Port.ToString(),
                ["quartz.scheduler.exporter.bindName"] = config.BindName,
                ["quartz.scheduler.exporter.channelType"] = "tcp",
                ["quartz.scheduler.exporter.channelName"] = "httpQuartz"
            };
            var schedulerFactory = new StdSchedulerFactory(nameValueCollection);
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            if (config.EnableCustomJobFactory)
            {
                var jobListener = _serviceProvider.GetService<IJobFactory>();
                if (jobListener != null) scheduler.JobFactory = jobListener;
            }
            if (config.EnableJobLog)
            {
                var jobListener = _serviceProvider.GetService<IJobListener>();
                if (jobListener != null) scheduler.ListenerManager.AddJobListener(jobListener);
            }
            if (config.EnableTriggerLog)
            {
                var jobListener = _serviceProvider.GetService<ITriggerListener>();
                if (jobListener != null) scheduler.ListenerManager.AddTriggerListener(jobListener);
            }
            if (config.EnableSchedulerLog)
            {
                var jobListener = _serviceProvider.GetService<ISchedulerListener>();
                if (jobListener != null) scheduler.ListenerManager.AddSchedulerListener(jobListener);
            }
            return scheduler;
        }
    }
}
