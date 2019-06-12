using Quartz;
using Quartz.Spi;
using System;

namespace Materal.Dispatcher.Server
{
    public class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MyJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }

    }
}
