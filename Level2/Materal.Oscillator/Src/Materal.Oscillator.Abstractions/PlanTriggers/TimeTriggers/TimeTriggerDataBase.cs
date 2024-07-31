namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间触发器数据基类
    /// </summary>
    /// <param name="name"></param>
    public abstract class TimeTriggerDataBase(string name) : DefaultData, ITimeTriggerData
    {
        /// <inheritdoc/>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <inheritdoc/>
        public string Name { get; set; } = name;
        /// <inheritdoc/>
        public abstract string GetDescriptionText();
    }
}
