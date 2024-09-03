using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 托管服务装饰器处理服务
    /// </summary>
    /// <param name="hostedService"></param>
    /// <param name="serviceDecorators"></param>
    internal class HostedServiceDecoratorHandlerService(IHostedService hostedService, IEnumerable<IHostedServiceDecorator> serviceDecorators) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
            {
                await serviceDecorator.OnStartAsync(cancellationToken);
            }
            try
            {
                await hostedService.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                bool isHandle = false;
                foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
                {
                    isHandle = await serviceDecorator.OnExceptionAsync(cancellationToken, ex);
                    if (isHandle) break;
                }
                if (!isHandle) throw;
            }
            foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
            {
                await serviceDecorator.OnStartedAsync(cancellationToken);
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
            {
                await serviceDecorator.OnStopAsync(cancellationToken);
            }
            try
            {
                await hostedService.StopAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                bool isHandle = false;
                foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
                {
                    isHandle = await serviceDecorator.OnExceptionAsync(cancellationToken, ex);
                    if (isHandle) break;
                }
                if (!isHandle) throw;
            }
            foreach (IHostedServiceDecorator serviceDecorator in serviceDecorators)
            {
                await serviceDecorator.OnStopedAsync(cancellationToken);
            }
        }
    }
}
