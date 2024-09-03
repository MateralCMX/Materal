using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器装饰器
    /// </summary>
    public class ExceptionInterceptorHostedServiceDecorator(ILogger<GlobalExceptionFilter>? logger = null) : IHostedServiceDecorator
    {
        /// <inheritdoc/>
        public async Task<bool> OnExceptionAsync(CancellationToken cancellationToken, Exception exception)
        {
            if (exception is MergeBlockModuleException or ValidationException)
            {
                if (exception is AggregateException aggregateException)
                {
                    exception = aggregateException.InnerException ?? exception;
                }
                logger?.LogError(exception, exception.Message);
            }
            else
            {
                using IDisposable? scope = logger?.BeginScope("MergeBlockHostedServiceException");
                logger?.LogCritical(exception, exception.Message);
            }
            return await Task.FromResult(true);
        }
        /// <inheritdoc/>
        public async Task OnStartAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
        /// <inheritdoc/>
        public async Task OnStartedAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
        /// <inheritdoc/>
        public async Task OnStopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
        /// <inheritdoc/>
        public async Task OnStopedAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
