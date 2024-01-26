namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// Oscillator服务
    /// </summary>
    public interface IOscillatorService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
        /// <summary>
        /// 立即初始化
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        Task InitAsync(string scheduleName, string workName);
        /// <summary>
        /// 立即初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task InitAsync<T>() where T : IOscillatorSchedule, new();
        /// <summary>
        /// 立即运行
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        Task RunNowAsync(string scheduleName);
        /// <summary>
        /// 立即运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task RunNowAsync<T>() where T : IOscillatorSchedule, new();
        /// <summary>
        /// 立即运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task RunNowAsync<T>(object data) where T : IOscillatorSchedule, new();
        /// <summary>
        /// 立即运行
        /// </summary>
        /// <typeparam name="TSchedule"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        Task RunNowAsync<TSchedule, TData>(TData data) where TSchedule : IOscillatorSchedule, new();
    }
}
