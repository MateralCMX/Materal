using Materal.Oscillator.Abstractions.Oscillators;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器主机
    /// </summary>
    public interface IOscillatorHost
    {
        /// <summary>
        /// 启动
        /// </summary>
        Task StartAsync();
        /// <summary>
        /// 停止
        /// </summary>
        Task StopAsync();
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        Task<bool> StartOscillatorAsync(IOscillatorData oscillatorData);
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillatorDatas"></param>
        /// <returns></returns>
        Task StartOscillatorAsync(IEnumerable<IOscillatorData> oscillatorDatas);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        Task<bool> StopOscillatorAsync(IOscillatorData oscillatorData);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillatorDatas"></param>
        /// <returns></returns>
        Task StopOscillatorAsync(IEnumerable<IOscillatorData> oscillatorDatas);
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        Task<bool> CanRuningAsync(IOscillatorData oscillatorData);
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        Task<bool> CanRuningAsync(JobKey jobKey);
    }
}
