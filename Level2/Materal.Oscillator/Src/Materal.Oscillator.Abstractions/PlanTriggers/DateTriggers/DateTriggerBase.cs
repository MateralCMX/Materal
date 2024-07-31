using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 时间触发器基类
    /// </summary>
    /// <typeparam name="TDateTriggerData"></typeparam>
    public abstract class DateTriggerBase<TDateTriggerData> : IDateTrigger
        where TDateTriggerData : class, IDateTriggerData, new()
    {
        /// <inheritdoc/>
        public Guid ID => Data.ID;
        /// <inheritdoc/>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取调度器类型数据名称失败");
        /// <inheritdoc/>
        public Type DataType => typeof(TDateTriggerData);
        /// <inheritdoc/>
        public IDateTriggerData DateTriggerData => Data;
        /// <summary>
        /// 调度器数据
        /// </summary>
        public TDateTriggerData Data { get; private set; } = new();
        /// <inheritdoc/>
        public virtual DateTimeOffset? GetDateStartTime(ITimeTrigger timeTrigger)
        {
            if (Data.Interval <= 0) return null;
            DateTimeOffset? result = timeTrigger.GetTriggerStartTime(Data.StartDate);
            return result;
        }
        /// <inheritdoc/>
        public virtual DateTimeOffset? GetDateEndTime(ITimeTrigger timeTrigger)
        {
            if (Data.Interval <= 0) return null;
            if (Data.EndDate == null) return null;
            DateTimeOffset? result = timeTrigger.GetTriggerEndTime(Data.EndDate.Value);
            return result;
        }
        /// <inheritdoc/>
        public virtual DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger timeTrigger)
        {
            if (Data.Interval <= 0) return null;
            DateTimeOffset? result = timeTrigger.GetNextRunTime(upRunTime);
            if (result != null) return result;
            DateOnly? nextRunDate = GetNextRunDate(upRunTime);
            if (nextRunDate == null) return null;
            if (Data.EndDate != null && nextRunDate > Data.EndDate) return null;
            result = timeTrigger.GetTriggerStartTime(nextRunDate.Value);
            return result;
        }
        /// <summary>
        /// 获得下次运行的日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected abstract DateOnly? GetNextRunDate(DateTimeOffset upRunTime);
        /// <inheritdoc/>
        public async Task SetDataAsync(IDateTriggerData data)
        {
            if (data is not TDateTriggerData trueData) throw new OscillatorException("数据类型错误");
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
