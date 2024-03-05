using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 合并块服务
    /// </summary>
    public class MergeBlockHostedService(IServiceProvider serviceProvider) : IHostedService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (IModuleInfo moduleInfo in MergeBlockHost.ModuleInfos)
            {
                await moduleInfo.ApplicationStartdAsync(serviceProvider);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
