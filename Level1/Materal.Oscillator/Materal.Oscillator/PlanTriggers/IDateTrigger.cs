namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 日期触发器
    /// </summary>
    public interface IDateTrigger
    {
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public string GetDescriptionText(IEveryDayTrigger everyDayTrigger);
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public DateTimeOffset? GetDateStartTime(IEveryDayTrigger everyDayTrigger);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public DateTimeOffset? GetDateEndTime(IEveryDayTrigger everyDayTrigger);
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, IEveryDayTrigger everyDayTrigger);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="dateTriggerData"></param>
        /// <returns></returns>
        public IDateTrigger Deserialization(string dateTriggerData);
    }
}
