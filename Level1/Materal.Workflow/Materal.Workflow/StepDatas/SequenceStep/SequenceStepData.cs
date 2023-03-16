using System.ComponentModel.DataAnnotations;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 顺序执行节点数据
    /// </summary>
    public class SequenceStepData : StepData, IStepData
    {
        /// <summary>
        /// 节点数据组
        /// </summary>
        [Required, MinLength(1)]
        public List<IStepData> StepDatas { get; set; } = new();
    }
}
