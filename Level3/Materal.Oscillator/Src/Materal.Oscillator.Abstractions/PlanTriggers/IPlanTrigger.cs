using Materal.Oscillator.Abstractions.Models;
using Quartz;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器
    /// </summary>
    public interface IPlanTrigger : IOscillatorOperationModel<IPlanTrigger>
    {
        /// <summary>
        /// 重复执行的
        /// </summary>
        public bool CanRepeated { get; }
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        public string GetDescriptionText();
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public ITrigger? CreateTrigger(string name, string group);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public ITrigger? CreateTrigger(TriggerKey triggerKey);
    }
}
