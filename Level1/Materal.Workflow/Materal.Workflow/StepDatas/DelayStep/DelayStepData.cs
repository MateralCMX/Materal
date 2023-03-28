using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 延时节点数据
    /// </summary>
    public class DelayStepData : StepData, IStepData
    {
        /// <summary>
        /// 延时时间
        /// </summary>
        [Required]
        public TimeSpan Delay { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public DelayStepData()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="delay"></param>
        public DelayStepData(TimeSpan delay)
        {
            Delay = delay;
        }
    }
}
