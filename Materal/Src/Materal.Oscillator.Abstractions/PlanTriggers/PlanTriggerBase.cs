
using Materal.Oscillator.Abstractions.Extensions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器基类
    /// </summary>
    /// <typeparam name="TPlanTriggerData"></typeparam>
    public abstract class PlanTriggerBase<TPlanTriggerData> : IPlanTrigger
        where TPlanTriggerData : class, IPlanTriggerData, new()
    {
        /// <inheritdoc/>
        public Guid ID => Data.ID;
        /// <inheritdoc/>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取调度器类型数据名称失败");
        /// <inheritdoc/>
        public Type DataType => typeof(TPlanTriggerData);
        /// <inheritdoc/>
        public IPlanTriggerData PlanTriggerData => Data;
        /// <summary>
        /// 调度器数据
        /// </summary>
        public TPlanTriggerData Data { get; private set; } = new();
        /// <inheritdoc/>
        public ITrigger? CreateTrigger(IOscillator oscillator) => CreateTrigger(oscillator.OscillatorData);
        /// <inheritdoc/>
        public ITrigger? CreateTrigger(IOscillatorData oscillatorData)
        {
            TriggerKey triggerKey = Data.GetTriggerKey(oscillatorData);
            return CreateTrigger(triggerKey);
        }
        /// <summary>
        /// 创建Trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected virtual ITrigger? CreateTrigger(TriggerKey triggerKey, DateTime startTime, DateTime? endTime = null)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(startTime);
            if (endTime.HasValue)
            {
                triggerBuilder = triggerBuilder.EndAt(endTime.Value);
            }
            if (Data.CanRepeated)
            {
                triggerBuilder = triggerBuilder.WithOscillatorSchedule(m => m.WithTriggerData(this).RepeatForever());
            }
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
        /// <inheritdoc/>
        public abstract ITrigger? CreateTrigger(TriggerKey triggerKey);
        /// <inheritdoc/>
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <inheritdoc/>
        public async Task SetDataAsync(IPlanTriggerData data)
        {
            if (data is not TPlanTriggerData trueData) throw new OscillatorException("数据类型错误");
            Data = trueData;
            await InitAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected virtual async Task InitAsync() => await Task.CompletedTask;
    }
}
