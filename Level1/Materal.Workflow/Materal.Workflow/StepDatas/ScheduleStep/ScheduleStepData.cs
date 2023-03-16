using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 计划节点数据
    /// </summary>
    public class ScheduleStepData : StepData, IStepData
    {
        /// <summary>
        /// 等待时间
        /// </summary>
        [Required]
        public TimeSpan Delay { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// 计划节点
        /// </summary>
        public IStepData StepData { get; set; } = new StartStepData();
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ScheduleStepData()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="delay"></param>
        public ScheduleStepData(TimeSpan delay)
        {
            Delay = delay;
        }
    }
}
