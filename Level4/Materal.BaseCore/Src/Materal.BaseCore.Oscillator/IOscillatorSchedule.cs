using Materal.Oscillator.Abstractions;

namespace Materal.BaseCore.Oscillator
{
    public interface IOscillatorSchedule
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        string ScheduleName { get; }
        /// <summary>
        /// 任务名称
        /// </summary>
        string WorkName { get; }
        /// <summary>
        /// 新增调度器
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<Guid> AddSchedule(IOscillatorHost host);
    }
}
