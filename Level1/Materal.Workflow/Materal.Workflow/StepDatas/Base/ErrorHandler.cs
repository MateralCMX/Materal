using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public sealed class ErrorHandler
    {
        /// <summary>
        /// 错误处理类型
        /// </summary>
        public ErrorHandlerTypeEnum HandlerType { get; set; } = ErrorHandlerTypeEnum.Stop;
        /// <summary>
        /// 重试间隔
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan? RetryInterval { get; set; }
    }
}
