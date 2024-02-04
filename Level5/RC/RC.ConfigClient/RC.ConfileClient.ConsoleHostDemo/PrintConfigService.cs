using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace RC.ConfileClient.ConsoleHostDemo
{
    public class PrintConfigService(IOptionsMonitor<DemoConfig> config) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Print Config Service Start.");
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}]ApplicationName: {config.CurrentValue.ApplicationName}");
                await Task.Delay(1000);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Print Config Service Stop.");
            await Task.CompletedTask;
        }
    }
}
