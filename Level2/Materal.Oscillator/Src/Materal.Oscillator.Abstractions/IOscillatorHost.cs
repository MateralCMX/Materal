using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

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
        /// <param name="oscillator"></param>
        /// <returns></returns>
        Task<bool> StartOscillatorAsync(IOscillator oscillator);
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillators"></param>
        /// <returns></returns>
        Task StartOscillatorAsync(IEnumerable<IOscillator> oscillators);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        Task<bool> StopOscillatorAsync(IOscillator oscillator);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillators"></param>
        /// <returns></returns>
        Task StopOscillatorAsync(IEnumerable<IOscillator> oscillators);
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        Task<bool> RunNowOscillatorAsync(IOscillator oscillator);
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="workDatas"></param>
        /// <returns></returns>
        Task RunNowWorkDataAsync(IEnumerable<IWorkData> workDatas);
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="workData"></param>
        /// <returns></returns>
        Task<bool> RunNowWorkDataAsync(IWorkData workData);
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="workDataType"></param>
        /// <returns></returns>
        Task<bool> RunNowWorkDataAsync(Type workDataType);
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <returns></returns>
        Task<bool> RunNowWorkDataAsync<T>() where T : IWorkData;
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="oscillators"></param>
        /// <returns></returns>
        Task RunNowOscillatorAsync(IEnumerable<IOscillator> oscillators);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="oscillator"></param>
        /// <param name="planTrigger"></param>
        /// <returns></returns>
        Task<bool> RunOscillatorAsync(IOscillator oscillator, IPlanTrigger planTrigger);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="planTrigger"></param>
        /// <returns></returns>
        Task<bool> RunWorkDataAsync(IWorkData workData, IPlanTrigger planTrigger);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workDataType"></param>
        /// <param name="planTrigger"></param>
        /// <returns></returns>
        Task<bool> RunWorkDataAsync(Type workDataType, IPlanTrigger planTrigger);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="planTrigger"></param>
        /// <returns></returns>
        Task<bool> RunWorkDataAsync<T>(IPlanTrigger planTrigger) where T : IWorkData;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        Task<bool> CanRuningAsync(IOscillator oscillator);
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        Task<bool> CanRuningAsync(JobKey jobKey);
    }
}
