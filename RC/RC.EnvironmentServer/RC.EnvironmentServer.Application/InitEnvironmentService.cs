using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// 初始化环境服务
    /// </summary>
    /// <param name="configurationItemService"></param>
    /// <param name="logger"></param>
    public class InitEnvironmentService(IConfigurationItemService configurationItemService, ILogger<InitEnvironmentService>? logger = null) : IHostedService
    {
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                await Task.Delay(5000);
                PolicyBuilder policyBuilder = Policy.Handle<Exception>();
                AsyncRetryPolicy retryPolicy = policyBuilder.WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(5), (ex, index, timeSpan) =>
                {
                    logger?.LogWarning(ex, $"[{index}]初始化失败,5秒后重试");
                });
                await retryPolicy.ExecuteAsync(async () =>
                {
                    await configurationItemService.InitAsync();
                    logger?.LogInformation("初始化完毕");
                });
            });
            await Task.CompletedTask;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}