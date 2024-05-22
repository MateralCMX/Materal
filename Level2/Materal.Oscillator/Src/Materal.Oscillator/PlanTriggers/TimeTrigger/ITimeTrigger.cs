namespace Materal.Oscillator.PlanTriggers.TimeTrigger
{
    /// <summary>
    /// 时间触发器
    /// </summary>
    public interface ITimeTrigger : IOscillatorData
    {
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTimeOffset? GetTriggerStartTime(Date date);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTimeOffset? GetTriggerEndTime(Date date);
        /// <summary>
        /// 获得下次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        string GetDescriptionText();
    }
}
