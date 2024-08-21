namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务数据基类
    /// </summary>
    public abstract class WorkDataBase(string name = "新任务") : DefaultData, IWorkData
    {
        /// <inheritdoc/>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <inheritdoc/>
        public string Name { get; set; } = name;
    }
}
