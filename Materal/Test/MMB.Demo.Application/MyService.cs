using Microsoft.Extensions.Hosting;

namespace MMB.Demo.Application
{
    public class MyService(ILogger<MyService>? logger = null) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger?.LogInformation("服务启动");
            await Task.CompletedTask;
            throw new MergeBlockModuleException("测试异常");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger?.LogInformation("服务停止");
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
