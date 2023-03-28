using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点数据
    /// </summary>
    public interface IStepData
    {
        /// <summary>
        /// 节点数据类型名称
        /// </summary>
        string StepDataType { get; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        string ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        string? Description { get; set; }
        /// <summary>
        /// 构建数据
        /// </summary>
        [Required]
        Dictionary<string,object?>? BuildData { get; set; }
    }
}
