using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleHostDemo.Services
{
    /// <summary>
    /// Hello World服务
    /// </summary>
    /// <param name="logger"></param>
    public class HelloWorldService(ILogger<HelloWorldService> logger) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken) => Task.Factory.StartNew(async () =>
        {
            int index = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation($"Hello World{index++}!");
                await Task.Delay(1000);
            }
        });
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
