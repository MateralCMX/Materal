namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间触发器基类
    /// </summary>
    /// <typeparam name="TTimeTriggerData"></typeparam>
    public abstract class TimeTriggerBase<TTimeTriggerData> : ITimeTrigger
        where TTimeTriggerData : class, ITimeTriggerData, new()
    {
        /// <inheritdoc/>
        public Guid ID => Data.ID;
        /// <inheritdoc/>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取调度器类型数据名称失败");
        /// <inheritdoc/>
        public Type DataType => typeof(TTimeTriggerData);
        /// <inheritdoc/>
        public ITimeTriggerData TimeTriggerData => Data;
        /// <summary>
        /// 调度器数据
        /// </summary>
        public TTimeTriggerData Data { get; private set; } = new();
        /// <inheritdoc/>
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <inheritdoc/>
        public abstract DateTimeOffset? GetTriggerEndTime(DateOnly date);
        /// <inheritdoc/>
        public abstract DateTimeOffset? GetTriggerStartTime(DateOnly date);
        /// <inheritdoc/>
        public async Task SetDataAsync(ITimeTriggerData data)
        {
            if (data is not TTimeTriggerData trueData) throw new OscillatorException("数据类型错误");
            Data = trueData;
            await InitAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task InitAsync() => await Task.CompletedTask;
    }
}
