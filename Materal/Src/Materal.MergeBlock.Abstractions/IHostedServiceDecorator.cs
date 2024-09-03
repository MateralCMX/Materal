namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 托管服务装饰器
    /// </summary>
    public interface IHostedServiceDecorator
    {
        /// <summary>
        /// 当托管服务启动时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnStartAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 当托管服务启动完成时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnStartedAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 当托管服务停止时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnStopAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 当托管服务停止完成时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task OnStopedAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 当异常发生时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task<bool> OnExceptionAsync(CancellationToken cancellationToken, Exception exception);
    }
}
