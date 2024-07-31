using Newtonsoft.Json;
using System.Collections;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器数据
    /// </summary>
    public interface IPlanTriggerData : IData
    {
        /// <summary>
        /// 映射表
        /// </summary>
        static Hashtable MapperTable { get; } = [];
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 重复执行的
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        bool CanRepeated { get; }
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        string GetDescriptionText();
    }
}
