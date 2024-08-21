using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器监听器
    /// </summary>
    public interface IOscillatorListener
    {
        /// <summary>
        /// 调度器主机启动前
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        Task OnOscillatorHostStartBeforAsync(IScheduler scheduler);
        /// <summary>
        /// 调度器主机停止后
        /// </summary>
        /// <returns></returns>
        Task OnOscillatorHostStopAfterAsync();
        /// <summary>
        /// 调度器注册
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        Task OnOscillatorRegisterAsync(IOscillatorData oscillatorData);
        /// <summary>
        /// 调度器反注册
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        Task OnOscillatorUnRegisterAsync(IOscillatorData oscillatorData);
        /// <summary>
        /// 调度器上下文初始化
        /// </summary>
        /// <param name="context"></param>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        Task OnOscillatorContextInitAsync(IOscillatorContext context, IOscillator oscillator);
        /// <summary>
        /// 调度器开始
        /// </summary>
        /// <param name="context"></param>
        /// <param name="oscillator"></param>
        Task OnOscillatorStartAsync(IOscillatorContext context, IOscillator oscillator);
        /// <summary>
        /// 调度器结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="oscillator"></param>
        Task OnOscillatorEndAsync(IOscillatorContext context, IOscillator oscillator);
        /// <summary>
        /// 任务开始
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkStartAsync(IOscillatorContext context, IWork work);
        /// <summary>
        /// 任务成功开始
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkSuccessStartAsync(IOscillatorContext context, IWork work);
        /// <summary>
        /// 任务成功结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkSuccessEndAsync(IOscillatorContext context, IWork work);
        /// <summary>
        /// 任务失败开始
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkFailStartAsync(IOscillatorContext context, IWork work);
        /// <summary>
        /// 任务失败结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkFailEndAsync(IOscillatorContext context, IWork work);
        /// <summary>
        /// 任务结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="work"></param>
        Task OnWorkEndAsync(IOscillatorContext context, IWork work);
    }
}
