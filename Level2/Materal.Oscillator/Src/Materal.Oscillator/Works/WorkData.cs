using Materal.Oscillator.Abstractions.Works;
using Newtonsoft.Json;

namespace Materal.Oscillator.Works
{

    /// <summary>
    /// 任务数据
    /// </summary>
    public abstract class WorkData<TWork>(string name = "新任务") : WorkData(typeof(TWork).Name, name), IWorkData
        where TWork : IWork
    {
    }
    /// <summary>
    /// 任务数据
    /// </summary>
    public abstract class WorkData(string workTypeName, string name = "新任务") : DefaultOscillatorData, IWorkData
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = name;
        /// <summary>
        /// 任务类型名称
        /// </summary>
        [JsonIgnore]
        public string WorkTypeName { get; } = workTypeName;
    }
}
