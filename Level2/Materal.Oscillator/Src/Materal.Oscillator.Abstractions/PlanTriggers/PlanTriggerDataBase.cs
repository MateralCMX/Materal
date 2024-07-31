
namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器数据基类
    /// </summary>
    /// <param name="name"></param>
    public abstract class PlanTriggerDataBase(string name) : DefaultData, IPlanTriggerData
    {
        /// <inheritdoc/>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <inheritdoc/>
        public string Name { get; set; } = name;
        /// <inheritdoc/>
        public bool Enable { get; set; } = true;
        /// <inheritdoc/>
        public abstract bool CanRepeated { get; }
        /// <inheritdoc/>
        public abstract string GetDescriptionText();
    }
}
